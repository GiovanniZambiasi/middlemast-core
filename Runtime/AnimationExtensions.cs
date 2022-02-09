using UnityEngine;

namespace MiddleMast
{
    public static class AnimationExtensions
    {
        public static float GetHighestKeyframeValue(this AnimationCurve curve)
        {
            float max = -float.MinValue;
            Keyframe[] keyFrames = curve.keys;

            for (int i = 0; i < keyFrames.Length; i++)
            {
                Keyframe keyFrame = keyFrames[i];

                if (keyFrame.value > max)
                {
                    max = keyFrame.value;
                }
            }

            return max;
        }
    }
}
