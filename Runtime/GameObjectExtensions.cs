using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiddleMast
{
    public static class GameObjectExtensions
    {
        private static readonly List<GameObject> _objects = new List<GameObject>(16);

        public static T GetComponentInChildrenNoRestrictions<T>(this GameObject target)
        {
            T component = target.GetComponent<T>();

            if (component != null)
            {
                return component;
            }

            for (int i = 0; i < target.transform.childCount; i++)
            {
                Transform child = target.transform.GetChild(i);
                component = GetComponentInChildrenNoRestrictions<T>(child.gameObject);

                if (component == null)
                {
                    continue;
                }

                return component;
            }

            return default;
        }

        public static void SetLayerWithChildren(this GameObject obj, int layerIndex)
        {
            obj.layer = layerIndex;

            for (int i = 0; i < obj.transform.childCount; i++)
            {
                Transform child = obj.transform.GetChild(i);
                child.gameObject.SetLayerWithChildren(layerIndex);
            }
        }

        public static T FindRootObjectOfType<T>()
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);

                if (scene.TryFindRootObjectOfType(out T obj))
                {
                    return obj;
                }
            }

            return default;
        }

        public static bool TryFindRootObjectOfType<T>(this Scene scene, out T obj)
        {
            _objects.Clear();

            scene.GetRootGameObjects(_objects);

            for (int i = 0; i < _objects.Count; i++)
            {
                GameObject root = _objects[i];

                if (root.TryGetComponent(out obj))
                {
                    return true;
                }
            }

            obj = default;

            return false;
        }

        public static void GetFirstLevelChildComponents<T>(this GameObject go, List<T> list)
        {
            Transform transform = go.transform;

            for (int i = 0; i < transform.childCount; ++i)
            {
                Transform child = transform.GetChild(i);

                if (child.TryGetComponent(out T component))
                {
                    list.Add(component);
                }
            }
        }
    }
}
