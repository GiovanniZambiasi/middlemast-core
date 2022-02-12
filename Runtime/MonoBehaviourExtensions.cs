using System.Collections.Generic;
using UnityEngine;

namespace MiddleMast
{
    public static class MonoBehaviourExtensions
    {
        public static void GetFirstLevelChildComponents<T>(this MonoBehaviour monoBehaviour, List<T> list)
        {
            monoBehaviour.gameObject.GetFirstLevelChildComponents(list);
        }
    }
}
