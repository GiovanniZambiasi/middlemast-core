using System.Linq;
using UnityEngine;

namespace MiddleMast
{
    public enum VectorDimensions
    {
        X,
        Y,
        Z,
    }

    public static class VectorExtensions
    {
        /// <summary>
        ///     Gets the quadrant that the current direction is contained in in a 360 circle, relative to <see cref="Vector3.forward"/>
        /// </summary>
        /// <param name="dir">Input Direction</param>
        /// <param name="divider">Number of quadrants the circle contains</param>
        /// <param name="startOffset">Offset to start for quadrant checking. 0.5f is half of a quadrant offset</param>
        public static int GetQuadrant(this Vector3 dir, int divider, float startOffset = 0f, bool clockWise = true)
        {
            return GetQuadrant(dir, Vector3.up, Vector3.forward, divider, startOffset, clockWise);
        }

        /// <summary>
        ///     Gets the quadrant that the current direction is contained in in a 360 circle
        /// </summary>
        /// <param name="dir">Input Direction</param>
        /// <param name="divider">Number of quadrants the circle contains</param>
        /// <param name="startOffset">Offset to start for quadrant checking. 0.5f is half of a quadrant offset</param>
        /// <param name="reference">The reference for the up and forward directions</param>
        public static int GetQuadrant(this Vector3 dir, Transform reference, int divider, float startOffset = 0f, bool clockWise = true)
        {
            return GetQuadrant(dir, reference.up, reference.forward, divider, startOffset, clockWise);
        }

        /// <summary>
        ///     Gets the quadrant that the current direction is contained in in a 360 circle
        /// </summary>
        /// <param name="dir">Input Direction</param>
        /// <param name="divider">Number of quadrants the circle contains</param>
        /// <param name="startOffset">Offset to start for quadrant checking. 0.5f is half of a quadrant offset</param>
        public static int GetQuadrant(this Vector3 dir, Vector3 referenceUp, Vector3 referenceForward, int divider, float startOffset = 0f, bool clockWise = true)
        {
            int index = 0;
            float step = 360f / divider;
            float currentAngle = startOffset * step;
            float min = 999;

            for (int i = 0; i < divider; i++)
            {
                Vector3 direction = Quaternion.Euler(referenceUp * currentAngle) * referenceForward;
                float angle = Vector3.Angle(dir, direction);

                if (angle < min)
                {
                    min = angle;
                    index = i;
                }

                currentAngle += clockWise ? step : -step;
            }

            return index;
        }

        /// <summary>
        ///     Gets the quadrant that the current direction is contained in in a 360 circle, relative to <see cref="Vector2.up"/>
        /// </summary>
        /// <param name="dir">Input Direction</param>
        /// <param name="divider">Number of quadrants the circle contains</param>
        /// <param name="startOffset">Offset to start for quadrant checking. 0.5f is half of a quadrant offset</param>
        /// <returns></returns>
        public static int GetQuadrant(this Vector2 dir, int divider, float startOffset, bool clockWise = true)
        {
            return GetQuadrant(dir, Vector2.up, divider, startOffset, clockWise);
        }

        /// <summary>
        ///     Gets the quadrant that the current direction is contained in in a 360 circle, relative to <see cref="Vector2.up"/>
        /// </summary>
        /// <param name="dir">Input Direction</param>
        /// <param name="divider">Number of quadrants the circle contains</param>
        /// <param name="startOffset">Offset to start for quadrant checking. 0.5f is half of a quadrant offset</param>
        /// <param name="distance">The angle from the perfect quadrant direction</param>
        /// <returns></returns>
        public static int GetQuadrant(this Vector2 dir, Vector2 referenceUp, int divider, float startOffset, bool clockWise = true)
        {
            int index = 0;
            float step = 360f / divider;
            float currentAngle = startOffset * step;
            float min = 999;

            Debug.DrawLine(Vector3.zero, dir, Color.green);

            for (int i = 0; i < divider; i++)
            {
                Vector2 direction = Quaternion.Euler(Vector3.back * currentAngle) * referenceUp;
                Debug.DrawLine(Vector3.zero, direction, i == 0 ? Color.red : new Color(0.9f, 0.54f, 0.54f));
                float angle = Vector2.Angle(dir, direction);

                if (angle < min)
                {
                    min = angle;
                    index = i;
                }

                currentAngle += clockWise ? step : -step;
            }

            return index;

            //int index = 0;
            //float step = 360f / divider;
            //float currentAngle = startOffset * step;
            //float min = 999;
            //distance = 999;

            //Debug.DrawLine(Vector3.zero, dir, Color.green);
            //for (int i = 0; i < divider; i++)
            //{
            //    Vector2 direction = Quaternion.Euler(Vector3.forward * currentAngle) * Vector2.up;
            //    Debug.DrawLine(Vector3.zero, direction, i == 0 ? Color.red : new Color(0.9f, 0.54f, 0.54f));
            //    float angle = Vector2.Angle(dir, direction);
            //    if (angle < min)
            //    {
            //        min = angle;
            //        index = i;
            //        distance = angle;
            //    }
            //    currentAngle += clockWise ? -step : step;
            //}

            //return index;
        }

        /// <summary>
        ///     Adds drag to a Vector3, and clamps the result so it always ends in zero. Drag value is NOT multiplied by delta time
        /// </summary>
        public static Vector3 AddDrag(this Vector3 vector, float drag)
        {
            if (vector == Vector3.zero)
            {
                return Vector3.zero;
            }

            // Stores all original signs of the vector
            Vector3 originalSigns = new Vector3(Mathf.Sign(vector.x), Mathf.Sign(vector.y), Mathf.Sign(vector.z));

            // Finds normalized values for all 3 axis
            Vector3 dragNormalized = vector.normalized.Absolute();

            // Vector is made absolute to simplify clamping
            Vector3 absolute = vector.Absolute();

            // Each axis is multiplied by the normalized drag values
            absolute.x = Mathf.Clamp(absolute.x - dragNormalized.x * drag, 0, Mathf.Infinity);
            absolute.y = Mathf.Clamp(absolute.y - dragNormalized.y * drag, 0, Mathf.Infinity);
            absolute.z = Mathf.Clamp(absolute.z - dragNormalized.z * drag, 0, Mathf.Infinity);

            return MultiplyVector3(absolute, originalSigns);
        }

        /// <summary>
        ///     Adds drag to a Vector3, and clamps the result so it always ends in zero. Drag value is NOT multiplied by delta time
        /// </summary>
        public static Vector3 AddDrag(this Vector3 vector, float drag, params VectorDimensions[] ignoreDimensions)
        {
            if (vector == Vector3.zero)
            {
                return Vector3.zero;
            }

            // Stores all original signs of the vector
            Vector3 originalSigns = new Vector3(Mathf.Sign(vector.x), Mathf.Sign(vector.y), Mathf.Sign(vector.z));

            // Finds normalized values for all 3 axis
            Vector3 dragNormalized = vector.normalized.Absolute();

            // Vector is made absolute to simplify clamping
            Vector3 absolute = vector.Absolute();

            // Each axis is multiplied by the normalized drag values
            if (!ignoreDimensions.Contains(VectorDimensions.X))
            {
                absolute.x = Mathf.Clamp(absolute.x - dragNormalized.x * drag, 0, Mathf.Infinity);
            }

            if (!ignoreDimensions.Contains(VectorDimensions.Y))
            {
                absolute.y = Mathf.Clamp(absolute.y - dragNormalized.y * drag, 0, Mathf.Infinity);
            }

            if (!ignoreDimensions.Contains(VectorDimensions.Z))
            {
                absolute.z = Mathf.Clamp(absolute.z - dragNormalized.z * drag, 0, Mathf.Infinity);
            }

            return MultiplyVector3(absolute, originalSigns);
        }

        /// <summary>
        ///     Returns a copy of the vector, with all dimensions turned positive
        /// </summary>
        public static Vector3 Absolute(this Vector3 vec)
        {
            return new Vector3(Mathf.Abs(vec.x), Mathf.Abs(vec.y), Mathf.Abs(vec.z));
        }

        /// <summary>
        ///     Performs a multiplication on each separate axis
        /// </summary>
        public static Vector3 MultiplyVector3(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

#region Set

        public static Vector3 Set(this Vector3 v, VectorDimensions dimension, float value)
        {
            switch (dimension)
            {
                case VectorDimensions.X:
                    v.x = value;

                    break;

                case VectorDimensions.Y:
                    v.y = value;

                    break;

                case VectorDimensions.Z:
                    v.z = value;

                    break;
            }

            return v;
        }

        public static Vector3 SetX(this Vector3 v, float value)
        {
            v.x = value;

            return v;
        }

        public static Vector3 SetY(this Vector3 v, float value)
        {
            v.y = value;

            return v;
        }

        public static Vector3 SetZ(this Vector3 v, float value)
        {
            v.z = value;

            return v;
        }

#endregion

        public static bool IsNear(this Vector3 a, Vector3 target, float tolerance)
        {
            return (target - a).sqrMagnitude <= tolerance * tolerance; // <= tolerance * tolerance;
        }

        public static bool IsGreaterThan(this Vector3 a, Vector3 b)
        {
            return a.sqrMagnitude > b.sqrMagnitude;
        }

        public static bool IsGreaterThan(this Vector3 a, float b)
        {
            return a.sqrMagnitude > b * b;
        }

        public static bool IsNear(this Vector2 a, Vector2 target, float tolerance)
        {
            return (target - a).sqrMagnitude <= tolerance * tolerance; // <= tolerance * tolerance;
        }

        public static bool IsGreaterThan(this Vector2 a, Vector2 b)
        {
            return a.sqrMagnitude > b.sqrMagnitude;
        }

        public static bool IsGreaterThan(this Vector2 a, float b)
        {
            return a.sqrMagnitude > b * b;
        }
    }
}
