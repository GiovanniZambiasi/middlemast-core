using UnityEngine;

namespace MiddleMast
{
    public static class LineRendererExtensions
    {
        public static void SetAlpha(this LineRenderer renderer, float alpha)
        {
            Gradient gradient = renderer.colorGradient;

            GradientAlphaKey[] alphaKeys = gradient.alphaKeys;

            for (int i = 0; i < alphaKeys.Length; i++)
            {
                GradientAlphaKey key = alphaKeys[i];
                key.alpha = alpha;
                alphaKeys[i] = key;
            }

            gradient.alphaKeys = alphaKeys;
            renderer.colorGradient = gradient;
        }

        public static void SetColor(this LineRenderer renderer, Color color)
        {
            Gradient gradient = renderer.colorGradient;

            GradientColorKey[] colorKeys = gradient.colorKeys;

            for (int i = 0; i < colorKeys.Length; i++)
            {
                GradientColorKey key = colorKeys[i];
                key.color = color;
                colorKeys[i] = key;
            }

            gradient.colorKeys = colorKeys;
            renderer.colorGradient = gradient;
        }

        public static void SetColorAndAlpha(this LineRenderer renderer, Color color, float alpha)
        {
            renderer.SetColor(color);
            renderer.SetAlpha(alpha);
        }

        public static void SetWidth(this LineRenderer renderer, float width)
        {
            renderer.widthCurve = AnimationCurve.Constant(0, 1, width);
        }

        public static float GetLength(this LineRenderer renderer)
        {
            float length = 0;

            for (int i = 0; i < renderer.positionCount - 1; i++)
            {
                length += Vector3.Distance(renderer.GetPosition(i), renderer.GetPosition(i + 1));
            }

            return length;
        }
    }
}
