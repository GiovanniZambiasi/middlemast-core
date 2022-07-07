using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MiddleMast
{
    public static class EventSystemExtensions
    {
        private static EventSystem _previousAssignedEventSystem;
        private static PointerEventData _eventData;
        private static List<RaycastResult> _raycastResults = new List<RaycastResult>(2);

        public static bool IsOverUI(this EventSystem eventSystem, Vector2 position)
        {
            PointerEventData eventData = GetPointerEventData(eventSystem);
            eventData.position = position;

            _raycastResults.Clear();

            eventSystem.RaycastAll(eventData, _raycastResults);

            return _raycastResults.Count > 0;
        }

        public static void FindComponentsInUI<T>(this EventSystem eventSystem, Vector2 position, List<T> componentList)
        {
            PointerEventData eventData = GetPointerEventData(eventSystem);
            eventData.position = position;

            _raycastResults.Clear();

            eventSystem.RaycastAll(eventData, _raycastResults);

            foreach (RaycastResult result in _raycastResults)
            {
                if (result.gameObject.TryGetComponent(out T component))
                {
                    componentList.Add(component);
                }
            }
        }

        private static PointerEventData GetPointerEventData(EventSystem eventSystem)
        {
            if (_previousAssignedEventSystem != eventSystem)
            {
                _previousAssignedEventSystem = eventSystem;

                _eventData = new PointerEventData(eventSystem);
            }

            return _eventData;
        }
    }
}
