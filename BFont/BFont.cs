using SharpFont;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BFont
{
	public class BFont
	{
		private static readonly Library library = new Library();

		public BGlyph[] Glyphs { get; set; }
		public Bitmap[] Pages { get; set; }
		public string Name { get; set; }
		public int LineHeight { get; set; }
		public bool IsBold { get; set; }
		public bool IsItalic { get; set; }
		public bool IsDistanceField { get; set; }

		public static BFont FromFile(string path, string characters, int size, int padding, int pageSize)
		{
			using (Face face = new Face(library, path))
			{
				return FromFace(face, characters, size, pageSize, renderGlyph =>
				{
					AddPadding(renderGlyph, padding);
					//GenerateSignedDistanceField(renderGlyph, padding - 1);
				});
			}
		}

		private static void AddPadding(RenderGlyph renderGlyph, int padding)
		{
			int width = Math.Max(0, renderGlyph.Bitmap.Width + padding * 2);
			int height = Math.Max(0, renderGlyph.Bitmap.Height + padding * 2);

			Bitmap result = new Bitmap(width, height);
			Graphics graphics = Graphics.FromImage(result);

			graphics.DrawImageUnscaled(renderGlyph.Bitmap, new Point(padding, padding));

			renderGlyph.Info.XOffset += padding;
			renderGlyph.Info.YOffset = height;

			renderGlyph.Bitmap = result;
		}

		private unsafe static void GenerateSignedDistanceField(RenderGlyph renderGlyph, int spread)
		{
			int width = renderGlyph.Bitmap.Width;
			int height = renderGlyph.Bitmap.Height;
			
			BitmapData bitmapData = renderGlyph.Bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

			int bufferLength = bitmapData.Height * bitmapData.Stride;

			uint* input = (uint*)bitmapData.Scan0;
			uint* output = (uint*)(Marshal.AllocHGlobal(bufferLength));

			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					uint color = input[x + y * width];

					bool inside = (color >> 24 & 0xff) != 0;
					int distanceSquared = int.MaxValue;

					for (int dx = -spread; dx <= spread; dx++)
					{
						for (int dy = -spread; dy <= spread; dy++)
						{
							int sx = x + dx;
							int sy = y + dy;

							if (dx == 0 || dy == 0 || sx < 0 || sx >= width || sy < 0 || sy >= height)
							{
								continue;
							}

							uint compareColor = input[sx + sy * width];
							bool compareInside = (compareColor >> 24 & 0xff) != 0;

							if (compareInside ^ inside)
							{
								int compareDistanceSquared = dx * dx + dy * dy;

								if (compareDistanceSquared < distanceSquared)
								{
									distanceSquared = compareDistanceSquared;
								}
							}
						}
					}

					float distance = inside ? 0 : (float)Math.Sqrt(distanceSquared);
					distance = MathHelper.Clamp(distance, 0, spread);
					byte alpha = (byte)(255 - (distance / spread) * 255);

					output[x + y * width] = (uint)((alpha << 24) | 0xffffff);
				}
			}

			Buffer.MemoryCopy((void*)output, (void*)input, bufferLength, bufferLength);

			Marshal.FreeHGlobal((IntPtr)output);

			renderGlyph.Bitmap.UnlockBits(bitmapData);
		}

		private static BFont FromFace(Face face, string characters, int size, int pageSize, Action<RenderGlyph> action = null)
		{
			char[] singleCharacters = characters.GroupBy(c => c).Select(g => g.First()).Where(c => !char.IsControl(c)).ToArray();
			List<RenderGlyph> renderGlyphs = new List<RenderGlyph>();

			face.SetCharSize(0, size, 72, 72);

			foreach (var character in singleCharacters)
			{
				face.LoadGlyph(face.GetCharIndex(character), LoadFlags.Default, LoadTarget.Mono);
				face.Glyph.RenderGlyph(RenderMode.Mono);



				Bitmap glyphBitmap = face.Glyph.Metrics.Height != 0 ? face.Glyph.Bitmap.ToGdipBitmap(Color.White) : new Bitmap((int)face.Glyph.Advance.X, 1);

				renderGlyphs.Add(new RenderGlyph
				{
					Bitmap = glyphBitmap,
					Info = new BGlyph
					{
						Character = character,
						XOffset = face.Glyph.BitmapLeft,
						YOffset = face.Glyph.BitmapTop,
						Width = (int)face.Glyph.Metrics.Width,
						Height = (int)face.Glyph.Metrics.Height,
						XAdvance = (int)face.Glyph.Advance.X,
						YAdvance = (int)face.Glyph.Advance.Y
					}
				});
			}

			renderGlyphs = renderGlyphs.OrderByDescending(g => g.Info.Height)
				.ThenBy(g => g.Info.Character).ToList();

			List<Bitmap> pages = new List<Bitmap>();
			List<BGlyph> glyphs = new List<BGlyph>();
			Point cursor = new Point(0, 0);
			int maxHeight = 0;

			Bitmap page;
			Graphics graphics;

			void CreateNewPage()
			{
				page = new Bitmap(pageSize, pageSize);
				graphics = Graphics.FromImage(page);

				pages.Add(page);
			}

			CreateNewPage();

			foreach (var renderGlyph in renderGlyphs)
			{
				action?.Invoke(renderGlyph);

				if (cursor.X + renderGlyph.Bitmap.Width > pageSize)
				{
					cursor.X = 0;
					cursor.Y += maxHeight;
					maxHeight = 0;

					if (cursor.Y + renderGlyph.Bitmap.Height > pageSize)
					{
						cursor.Y = 0;
						CreateNewPage();
					}
				}

				renderGlyph.Info.Page = pages.Count - 1;
				renderGlyph.Info.X = cursor.X;
				renderGlyph.Info.Y = cursor.Y;

				glyphs.Add(renderGlyph.Info);

				graphics.DrawImageUnscaled(renderGlyph.Bitmap, cursor);

				maxHeight = Math.Max(maxHeight, renderGlyph.Bitmap.Height);
				cursor.X += renderGlyph.Bitmap.Width;
			}

			return new BFont
			{
				Glyphs = glyphs.ToArray(),
				LineHeight = face.Height,
				Pages = pages.ToArray(),
				Name = face.FamilyName
			};
		}

		private class RenderGlyph
		{
			public BGlyph Info { get; set; }
			public Bitmap Bitmap { get; set; }
		}
	}

	//TODO: Add kerning information
	public class BGlyph
	{
		public int Character { get; set; }
		public int XAdvance { get; set; }
		public int YAdvance { get; set; }

		public int XOffset { get; set; }
		public int YOffset { get; set; }

		public int X { get; set; }
		public int Y { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }

		public int Page { get; set; }
	}
}
