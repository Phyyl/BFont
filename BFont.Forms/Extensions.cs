using BetterFont;
using System;
using System.Drawing;

namespace BFontCore.Forms
{
	public static class Extensions
	{
		public static unsafe Bitmap ToBitmap(this BBitmap source)
		{
			Bitmap bitmap = new Bitmap(source.Width, source.Height);

			var bitmapData = bitmap.LockBits(new Rectangle(0, 0, source.Width, source.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			fixed (uint* ptr = source.Buffer)
			{
				Buffer.MemoryCopy(ptr, (void*)bitmapData.Scan0, source.Length * 4, source.Length * 4);
			}
			
			bitmap.UnlockBits(bitmapData);

			return bitmap;
		}
	}
}
