using UnityEngine;

namespace MiddleMast
{
    public static class MathExtensions
    {
        /// <summary>
        ///     Returns the numeric distance between a and b
        ///     <para>Returns 0 if they are the same.</para>
        ///     <para>If a = 1 and b = 3, will return 2</para>
        ///     <para>If a = -1 and b = 3, will return 4</para>
        /// </summary>
        public static float Distance(float a, float b)
        {
            if (Mathf.Sign(a) == Mathf.Sign(b))
            {
                a = Mathf.Abs(a);
                b = Mathf.Abs(b);

                return a > b ? a - b : b - a;
            }

            return a < 0 ? b + Mathf.Abs(a) : a + Mathf.Abs(b);
        }

        public static bool Within(this float value, float low, float high, bool inclusive = true)
        {
            return inclusive ? value >= low && value <= high : value > low && value < high;
        }

        public static bool Within(this int value, int low, int high, bool inclusive = true)
        {
            return inclusive ? value >= low && value <= high : value > low && value < high;
        }

        public static bool SameSign(float a, float b)
        {
            return Mathf.Sign(a) == Mathf.Sign(b);
        }

        public static bool IsEqual(this float v, float other)
        {
            return v >= other - Mathf.Epsilon && v <= other + Mathf.Epsilon;
        }

        public static bool IsApproximately(this float v, float other, float tolerance)
        {
            float distance = Distance(v, other);

            return distance <= tolerance;
        }

        public static float ClampMax(this float value, float max)
        {
            return value > max ? max : value;
        }

        public static float ClampMin(this float value, float min)
        {
            return value < min ? min : value;
        }

        public static float Clamp(this float v, float min, float max)
        {
            return v.ClampMin(min).ClampMax(max);
        }

        public static float Clamp01(this float v)
        {
            return v.Clamp(0f, 1f);
        }

        public static float Damp(this float v, float damp)
        {
            float sign = Mathf.Sign(v);
            float abs = v.Absolute();
            v = abs - damp;

            return v * sign;
        }

        /// <summary>
        ///     Makes sure value is never greater than length, or less than 0
        /// </summary>
        public static float RepeatValue(float value, float length)
        {
            length = length.Absolute();

            if (value < 0)
            {
                float reduce = (value % length).Absolute();

                return length - reduce;
            }

            return value % length;
        }

        /// <summary>
        ///     Makes sure value is never greater than or equal to length [exclusive], or less than 0
        /// </summary>
        public static float Repeat(this float v, float length)
        {
            return RepeatValue(v, length);
        }

        /// <summary>
        ///     Returns the unsigned version of the value
        /// </summary>
        public static float Absolute(this float v)
        {
            return v < 0 ? -v : v;
        }

        public static int Normalize(this float v, float tolerance)
        {
            if (v.Absolute() <= tolerance)
            {
                return 0;
            }

            return v > 0 ? 1 : -1;
        }

        /// <summary>
        ///     Makes sure value is never greater than or equal to length [exclusive], or less than 0
        /// </summary>
        public static int RepeatValue(int value, int length)
        {
            length = length.Absolute();

            if (value < 0)
            {
                int reduce = (value % length).Absolute();

                return reduce == 0 ? length - 1 : length - reduce;
            }

            return value % length;
        }

        /// <summary>
        ///     Makes sure value is never greater than or equal to length [exclusive], or less than 0
        /// </summary>
        public static int Repeat(this int v, int length)
        {
            return RepeatValue(v, length);
        }

        /// <summary>
        ///     Returns the unsigned version of the value
        /// </summary>
        public static int Absolute(this int v)
        {
            return v < 0 ? -v : v;
        }

        public static int Clamp(this int v, int min, int max)
        {
            return v.ClampMin(min).ClampMax(max);
        }

        public static int ClampMax(this int value, int max)
        {
            return value > max ? max : value;
        }

        public static int ClampMin(this int value, int min)
        {
            return value < min ? min : value;
        }
    }
}
