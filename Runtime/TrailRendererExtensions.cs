using UnityEngine;

namespace MiddleMast
{
    public static class TrailRendererExtensions
    {
        /// <summary>
        ///     Sets all colors of the given Trail Renderer uniformly
        /// </summary>
        public static void SetColorUniform(this TrailRenderer r, Color color)
        {
            Gradient grad = r.colorGradient;
            GradientColorKey[] keys =
            {
                new GradientColorKey(color, 0),
            };

            grad.colorKeys = keys;
            r.colorGradient = grad;
        }
    }
}
