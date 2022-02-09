using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MiddleMast
{
    public static class TransformExtensions
    {
        public static Transform FindClosest(this Transform t, Transform[] others)
        {
            if (others.Length == 0)
            {
                throw new IndexOutOfRangeException("Other transforms is of length 0!");
            }

            Transform closest = others[0];

            if (others.Length == 1)
            {
                return closest;
            }

            float shortDistance = Vector3.SqrMagnitude(closest.position - t.position);

            for (int i = 1; i < others.Length; i++)
            {
                float distance = Vector3.SqrMagnitude(others[i].position - t.position);

                if (distance >= shortDistance)
                {
                    continue;
                }

                closest = others[i];
                shortDistance = distance;
            }

            return closest;
        }

        public static T FindClosest<T>(this Transform t, IEnumerable<T> others)
            where T : MonoBehaviour
        {
            if (others.Count() == 0)
            {
                throw new IndexOutOfRangeException("Other transforms is of length 0!");
            }

            T closest = others.ElementAt(0);

            if (others.Count() == 1)
            {
                return closest;
            }

            float shortDistance = Vector3.SqrMagnitude(closest.transform.position - t.position);

            for (int i = 1; i < others.Count(); i++)
            {
                float distance = Vector3.SqrMagnitude(others.ElementAt(i).transform.position - t.position);

                if (distance >= shortDistance)
                {
                    continue;
                }

                closest = others.ElementAt(i);
                shortDistance = distance;
            }

            return closest;
        }

        public static bool IsNear(this Transform t, Vector3 position, float tolerance)
        {
            return t.transform.position.IsNear(position, tolerance);
        }

        public static bool IsNear(this Transform t, Transform other, float tolerance)
        {
            return t.transform.position.IsNear(other.position, tolerance);
        }

        public static void Copy(this Transform t, Transform other)
        {
            t.position = other.position;
            t.rotation = other.rotation;
        }
    }
}
