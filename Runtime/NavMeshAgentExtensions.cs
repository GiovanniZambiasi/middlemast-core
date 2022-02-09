using UnityEngine;
using UnityEngine.AI;

namespace MiddleMast
{
    public static class NavMeshAgentExtensions
    {
        private static readonly Vector3[] Positions = new Vector3[50];

        public static int RemainingCornerCount(this NavMeshAgent agent)
        {
            return agent.path.GetCornersNonAlloc(Positions);
        }

        public static Vector3 GetCurrentTargetCorner(this NavMeshAgent agent)
        {
            int count = agent.path.GetCornersNonAlloc(Positions);

            return count == 0 ? default : Positions[0];
        }
    }
}
