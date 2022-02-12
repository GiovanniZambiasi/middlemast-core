using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiddleMast
{
    public static class SceneExtensions
    {
        private static List<GameObject> _objects = new List<GameObject>();

        /// <summary>
        ///     Recursively searches the scene and tries to find the given component
        /// </summary>
        public static T FindObjectOfTypeNoRestriction<T>(this Scene scene, bool includeInactive = true)
        {
            if (!includeInactive)
            {
                Debug.LogWarning("<color=red>Exclude inactives not implemented!</color> Return value could still be valid though");
            }

            GameObject[] roots = scene.GetRootGameObjects();

            for (int j = 0; j < roots.Length; j++)
            {
                GameObject root = roots[j];
                T component = root.GetComponentInChildrenNoRestrictions<T>();

                if (component == null)
                {
                    continue;
                }

                return component;
            }

            return default;
        }

        public static T FindRootObjectOfType<T>(this Scene scene, bool includeInactive = true)
        {
            _objects.Clear();
            scene.GetRootGameObjects(_objects);

            for (int i = 0; i < _objects.Count; i++)
            {
                GameObject root = _objects[i];

                if (!root.activeSelf && !includeInactive)
                {
                    continue;
                }

                if (root.TryGetComponent(out T component))
                {
                    return component;
                }
            }

            return default;
        }
    }
}
