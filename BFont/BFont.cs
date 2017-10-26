using SharpFont;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BFontCore
{
	public class BFont
	{
		private static Library library;

		public BGlyph[] Glyphs { get; set; }
		public BBitmap[] Pages { get; set; }
		public string Name { get; set; }
		public int LineHeight { get; set; }
		public bool IsBold { get; set; }
		public bool IsItalic { get; set; }
		public bool IsDistanceField { get; set; }

		static BFont()
		{
			NativeLibraryHelper.LoadEmbeddedLibrary("BFontCore", "freetype6.dll");

			library = new Library();
		}

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

			BBitmap result = new BBitmap(width, height);

			result.Draw(renderGlyph.Bitmap, padding, padding);

			renderGlyph.Info.XOffset += padding;
			renderGlyph.Info.YOffset = height;

			renderGlyph.Bitmap = result;
		}

		//private static void GenerateSignedDistanceField(RenderGlyph renderGlyph, int spread)
		//{
		//	int width = renderGlyph.Bitmap.Width;
		//	int height = renderGlyph.Bitmap.Height;

		//	int bufferLength = bitmapData.Height * bitmapData.Stride;

		//	uint* input = (uint*)bitmapData.Scan0;
		//	uint* output = (uint*)(Marshal.AllocHGlobal(bufferLength));

		//	for (int x = 0; x < width; x++)
		//	{
		//		for (int y = 0; y < height; y++)
		//		{
		//			uint color = input[x + y * width];

		//			bool inside = (color >> 24 & 0xff) != 0;
		//			int distanceSquared = int.MaxValue;

		//			for (int dx = -spread; dx <= spread; dx++)
		//			{
		//				for (int dy = -spread; dy <= spread; dy++)
		//				{
		//					int sx = x + dx;
		//					int sy = y + dy;

		//					if (dx == 0 || dy == 0 || sx < 0 || sx >= width || sy < 0 || sy >= height)
		//					{
		//						continue;
		//					}

		//					uint compareColor = input[sx + sy * width];
		//					bool compareInside = (compareColor >> 24 & 0xff) != 0;

		//					if (compareInside ^ inside)
		//					{
		//						int compareDistanceSquared = dx * dx + dy * dy;

		//						if (compareDistanceSquared < distanceSquared)
		//						{
		//							distanceSquared = compareDistanceSquared;
		//						}
		//					}
		//				}
		//			}

		//			float distance = inside ? 0 : (float)Math.Sqrt(distanceSquared);
		//			distance = MathHelper.Clamp(distance, 0, spread);
		//			byte alpha = (byte)(255 - (distance / spread) * 255);

		//			output[x + y * width] = (uint)((alpha << 24) | 0xffffff);
		//		}
		//	}

		//	Buffer.MemoryCopy((void*)output, (void*)input, bufferLength, bufferLength);

		//	Marshal.FreeHGlobal((IntPtr)output);

		//	renderGlyph.Bitmap.UnlockBits(bitmapData);
		//}

		private static BFont FromFace(Face face, string characters, int size, int pageSize, Action<RenderGlyph> action = null)
		{
			char[] singleCharacters = characters.GroupBy(c => c).Select(g => g.First()).Where(c => !char.IsControl(c)).ToArray();
			List<RenderGlyph> renderGlyphs = new List<RenderGlyph>();

			face.SetCharSize(0, size, 72, 72);

			foreach (var character in singleCharacters)
			{
				face.LoadGlyph(face.GetCharIndex(character), LoadFlags.Default, LoadTarget.Mono);
				face.Glyph.RenderGlyph(RenderMode.Normal);
				
				BBitmap glyphBitmap = face.Glyph.Metrics.Height != 0 ? BBitmap.FromFTBitmap(face.Glyph.Bitmap) : new BBitmap((int)face.Glyph.Advance.X, 0);

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

			List<BBitmap> pages = new List<BBitmap>();
			List<BGlyph> glyphs = new List<BGlyph>();
			Point cursor = new Point(0, 0);
			int maxHeight = 0;

			BBitmap page;

			void CreateNewPage()
			{
				page = new BBitmap(pageSize, pageSize);

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

				page.Draw(renderGlyph.Bitmap, cursor.X, cursor.Y);

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
			public BBitmap Bitmap { get; set; }
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
