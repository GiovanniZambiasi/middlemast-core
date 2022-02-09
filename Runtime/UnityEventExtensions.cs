using UnityEngine.Events;
#if UNITY_EDITOR
using static UnityEditor.Events.UnityEventTools;
#endif

namespace MiddleMast
{
    public static class UnityEventExtensions
    {
#if UNITY_EDITOR
        /// <summary>
        ///     This is an EDITOR-ONLY function that removes all persistent listeners (i.e. listeners that appear in the inspector)
        ///     of an event
        /// </summary>
        /// <param name="e"></param>
        public static void RemoveAllPersistentListeners(this UnityEventBase e)
        {
            for (int i = e.GetPersistentEventCount() - 1; i >= 0; i--)
            {
                RemovePersistentListener(e, i);
            }
        }
#endif
    }
}
