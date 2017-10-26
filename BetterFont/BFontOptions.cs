using System;
using System.Collections.Generic;
using System.Text;

namespace BetterFont
{
	public class BFontOptions
	{
		private int padding;
		private int distanceField;
		private int size;
		private int pageSize;
		private string characters;
		
		public int Padding
		{
			get => padding;
			set => padding = MathHelper.Clamp(value, 0, byte.MaxValue);
		}

		public int DistanceField
		{
			get => distanceField;
			set => distanceField = MathHelper.Clamp(value, 0, 32);
		}

		public int Size
		{
			get => size;
			set => size = MathHelper.Clamp(value, 1, short.MaxValue);
		}

		public int PageSize
		{
			get => pageSize;
			set => pageSize = MathHelper.Clamp(value, 128, short.MaxValue);
		}

		public string Characters
		{
			get => characters;
			set => characters = value ?? "";
		}
	}
}
