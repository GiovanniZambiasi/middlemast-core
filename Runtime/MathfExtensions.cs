namespace MiddleMast
{
    public static class MathfExtensions
    {
        /// <summary>
        /// Returns the numeric difference between a and b
        /// <para>Returns 0 if they are the same.</para>
        /// <para>If a = 1 and b = 3, will return 2</para>
        /// <para>If a = -1 and b = 3, will return 4</para>
        /// </summary>
        public static float Distance(float a, float b)
        {
            return (a - b).Absolute();
        }

        public static bool InBetween(this float value, float low, float high, bool inclusive = true)
        {
            return inclusive ? value >= low && value <= high : value > low && value < high;
        }

        public static bool InBetween(this int value, int low, int high, bool inclusive = true)
        {
            return inclusive ? value >= low && value <= high : value > low && value < high;
        }
    }
}
