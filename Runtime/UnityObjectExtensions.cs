using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiddleMast
{
    public static class UnityObjectExtensions
    {
        /// <summary>
        ///     Same as <see cref="Object.FindObjectsOfType{T}()"/>, but this method includes inactive objects
        /// </summary>
        public static T[] FindObjectsOfTypeIncInactive<T>(bool includeInactive)
            where T : Component
        {
            List<T> results = new List<T>();

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                GameObject[] roots = scene.GetRootGameObjects();

                for (int j = 0; j < roots.Length; j++)
                {
                    results.AddRange(roots[j].GetComponentsInChildren<T>(includeInactive));
                }
            }

            return results.ToArray();
        }

        /// <summary>
        ///     Same as <see cref="Object.FindObjectsOfType{T}()"/>, but this method includes inactive objects
        /// </summary>
        public static T[] FindObjectsOfTypeIncInactive<T>(this Object o, bool includeInactive)
            where T : Component
        {
            return FindObjectsOfTypeIncInactive<T>(includeInactive);
        }

        /// <summary>
        ///     Same as <see cref="Object.FindObjectOfType{T}()"/>, but this method includes inactive objects
        /// </summary>
        public static T FindObjectOfTypeIncInactive<T>(this Object go, bool includeInactive)
            where T : Component
        {
            return FindObjectOfTypeNoRestriction<T>(includeInactive);
        }

        public static T FindObjectOfTypeNoRestriction<T>(bool includeInactive = false)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                T obj = scene.FindObjectOfTypeNoRestriction<T>(includeInactive);

                if (obj != null)
                {
                    return obj;
                }
            }

            return default;
        }

        public static T FindObjectOfTypeNoRestriction<T>(this Object o, bool includeInactive = false)
        {
            return FindObjectOfTypeNoRestriction<T>();
        }

        public static bool IsNullComplex<T>(T obj)
        {
            if (obj == null)
            {
                return true;
            }

            if (obj is Object unityObj)
            {
                return unityObj == null;
            }

            return true;
        }
    }
}
