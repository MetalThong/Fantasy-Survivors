using System.Collections.Generic;
using UnityEngine;

namespace Features.Combat
{
    public struct SteeringContext
    {
        public Vector3 CurrentPosition;
        public Vector3 TargetPosition;
        public List<CombatEntity> Neighbors;

        public SteeringContext(Vector3 currentPosition, Vector3 targetPosition, List<CombatEntity> neighbors)
        {
            CurrentPosition = currentPosition;
            TargetPosition = targetPosition;
            Neighbors = neighbors;
        }
    }
}