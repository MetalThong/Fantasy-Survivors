using System.Collections.Generic;
using UnityEngine;

namespace Features.Combat
{
    public interface INeighborProvider
    {
        void CollectNeighbors(List<CombatEntity> neighbors, Vector3 currentPositon, float radius);
    }
}