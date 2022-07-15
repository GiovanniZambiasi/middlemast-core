using System.Collections.Generic;
using UnityEngine;

namespace MiddleMast
{
    public class GameObjectComponentCache<TComponent>
    {
        Dictionary<int, TComponent> _dictionary;
        HashSet<int> _invalids;

        public GameObjectComponentCache()
        {
            _dictionary = new Dictionary<int, TComponent>(4);
            _invalids = new HashSet<int>();
        }

        public bool TryGetComponent(GameObject gameObject, out TComponent component)
        {
            int instanceID = gameObject.GetHashCode();

            if (_dictionary.ContainsKey(instanceID))
            {
                component = _dictionary[instanceID];
                return true;
            }

            if (_invalids.Contains(instanceID))
            {
                component = default;
                return false;
            }

            if (gameObject.TryGetComponent(out component))
            {
                _dictionary.Add(instanceID, component);
                return true;
            }
            else
            {
                _invalids.Add(instanceID);
                return false;
            }
        }

        public void Clear()
        {
            _dictionary.Clear();
            _invalids.Clear();
        }
    }
}
