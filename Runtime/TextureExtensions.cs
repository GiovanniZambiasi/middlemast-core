using UnityEngine;

namespace MiddleMast
{
    public static class TextureExtensions
    {
        public static void FlipTextureVertically(this Texture2D tex)
        {
            int iterations = Mathf.CeilToInt(tex.height / 2f);

            int highestVerticalIndex = tex.height - 1;

            for (int y = 0; y < iterations; y++)
            {
                int mirroredY = highestVerticalIndex - y;

                for (int x = 0; x < tex.width; x++)
                {
                    Color pixel = tex.GetPixel(x, y);

                    Color other = tex.GetPixel(x, mirroredY);

                    tex.SetPixel(x, y, other);

                    tex.SetPixel(x, mirroredY, pixel);
                }
            }
        }
    }
}
