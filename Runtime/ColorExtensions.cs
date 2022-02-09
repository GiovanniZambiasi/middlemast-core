using UnityEngine;

namespace MiddleMast
{
    public static class ColorExtensions
    {
        public static Color SetAlpha(this Color c, float alpha)
        {
            c.a = alpha;
            return c;
        }
    }
}
