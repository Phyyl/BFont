using SharpFont;
using System;
using System.Runtime.CompilerServices;

namespace BetterFont
{
	public class BBitmap
	{
		public uint[] Buffer { get; private set; }

		public int Width { get; private set; }
		public int Height { get; private set; }
		public int Stride { get; private set; }

		public int PixelStride => Stride / sizeof(int);
		public int Length => Buffer.Length;

		public uint this[int x, int y]
		{
			get => Buffer[x + y * PixelStride];
			set => Buffer[x + y * PixelStride] = value;
		}

		public BBitmap(int width, int height)
			: this(width, height, width * sizeof(uint))
		{

		}

		private BBitmap(int width, int height, int stride)
		{
			if (width < 0) throw new ArgumentOutOfRangeException(nameof(width));
			if (height < 0) throw new ArgumentOutOfRangeException(nameof(height));

			Width = width;
			Height = height;
			Stride = stride;

			Buffer = new uint[Width * Height];
		}

		internal void Draw(BBitmap source, int x, int y)
		{
			for (int xx = 0; xx < source.Width; xx++)
			{
				for (int yy = 0; yy < source.Height; yy++)
				{
					this[x + xx, y + yy] = source[xx, yy];
				}
			}
		}

		internal static unsafe BBitmap FromFTBitmap(FTBitmap bitmap)
		{
			BBitmap result = new BBitmap(bitmap.Width, bitmap.Rows);

			Copy(bitmap.Buffer, result.Buffer, result.Width, result.Height, bitmap.Pitch, GetBpp(bitmap.PixelMode));

			return result;
		}

		private static unsafe void Copy(IntPtr source, uint[] destination, int width, int height, int stride, int sourceBpp)
		{
			int bytesPerPixel = sourceBpp / 8;

			if (bytesPerPixel == 0)
			{
				CopySubByte(source, destination, width, height, stride, sourceBpp);
				return;
			}

			fixed (uint* dst = destination)
			{
				byte* src = (byte*)source;

				for (int y = 0; y < height; y++)
				{
					for (int x = 0; x < width; x++)
					{
						uint value = 0xFF;

						for (int j = 0; j < bytesPerPixel; j++)
						{
							value = (value << 8) | src[x + y * stride + j];
						}

						dst[x + y * width] = ToARGB(value, sourceBpp);
					}
				}
			}
		}

		private static unsafe void CopySubByte(IntPtr source, uint[] destination, int width, int height, int stride, int sourceBpp)
		{
			fixed (uint* dst = destination)
			{
				byte* src = (byte*)source;

				int pixelPerByte = 8 / sourceBpp;

				uint mask = (uint)((1 << sourceBpp) - 1);

				for (int y = 0; y < height; y++)
				{
					for (int x = 0; x < width; x++)
					{
						int bitX = x / pixelPerByte;
						int bitOffset = (x % pixelPerByte) * sourceBpp;
						int shift = 8 - bitOffset - sourceBpp;

						uint value = ((uint)src[y * stride + bitX] >> shift) & mask;

						dst[x + y * width] = ToARGB(value, sourceBpp);
					}
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint ToARGB(uint color, int bpp)
		{
			switch (bpp)
			{
				case 1:
				case 4:
				case 8:
					float a = color / ((1 << bpp) - 1f);
					return ((uint)(a * 255) << 24) | 0xFFFFFF;
				default:
					return color;
			}
		}

		private static int GetBpp(PixelMode pixelMode)
		{
			switch (pixelMode)
			{
				case PixelMode.Mono:
					return 1;
				case PixelMode.Gray:
					return 8;
				case PixelMode.Gray2:
					return 2;
				case PixelMode.Gray4:
					return 4;
				case PixelMode.Bgra:
					return 32;
				default:
					return 24;

			}
		}
	}
}
