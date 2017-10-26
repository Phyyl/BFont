using System.Runtime.CompilerServices;

namespace BetterFont
{
	internal static class MathHelper
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Clamp(float value, float min, float max) => value < min ? min : (value > max ? max : value);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Clamp(int value, int min, int max) => value < min ? min : (value > max ? max : value);
	}
}
