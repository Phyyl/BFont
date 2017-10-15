using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFont
{
    internal static class MathHelper
    {
        public static float Clamp(float value, float min, float max) => Math.Max(Math.Min(value, max), min);
        public static int Clamp(int value, int min, int max) => Math.Max(Math.Min(value, max), min);
    }
}
