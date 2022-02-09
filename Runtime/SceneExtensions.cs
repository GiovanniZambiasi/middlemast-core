using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiddleMast
{
    public static class SceneExtensions
    {
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

        public static T GetComponent<T>(this Scene scene, bool includeInactive = false)
        {
            GameObject[] roots = scene.GetRootGameObjects();

            for (int i = 0; i < roots.Length; i++)
            {
                GameObject root = roots[i];

                if (!root.activeInHierarchy && !includeInactive)
                {
                    continue;
                }

                T component = root.GetComponent<T>();

                if (component == null)
                {
                    continue;
                }

                return component;
            }

            return default;
        }
    }
}
