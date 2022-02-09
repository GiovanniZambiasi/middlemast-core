using UnityEngine;

namespace MiddleMast
{
    public static class ParticleSystemExtensions
    {
        public static float GetDuration(this ParticleSystem ps)
        {
            float maxParticleLifetime = ps.main.startLifetime.GetMax();

            return ps.main.duration + maxParticleLifetime;
        }

        public static float GetMax(this ParticleSystem.MinMaxCurve minMax)
        {
            float max = 0f;

            switch (minMax.mode)
            {
                case ParticleSystemCurveMode.Constant:
                    return minMax.constant;

                case ParticleSystemCurveMode.TwoConstants:
                    return minMax.constantMax;

                    break;

                /* Please note that evaluating the *actual* min and max values of the curves isn't very performatic.
                 This script will only compare the values in the keys, disregarding tangent oscilations.*/
                case ParticleSystemCurveMode.Curve:
                    return minMax.curve.GetHighestKeyframeValue();

                case ParticleSystemCurveMode.TwoCurves:

                    max = minMax.curveMax.GetHighestKeyframeValue();

                    float minCurveMaxValue = minMax.curveMin.GetHighestKeyframeValue();

                    if (minCurveMaxValue > max)
                    {
                        max = minCurveMaxValue;
                    }

                    return max;
            }

            return max;
        }
    }
}
