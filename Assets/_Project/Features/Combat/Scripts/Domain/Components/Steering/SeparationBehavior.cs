using UnityEngine;

namespace Features.Combat
{
    public class SeparationBehavior : ISteeringBehavior
    {
        private readonly float _separationWeight;
        
        public SeparationBehavior(float separationWeight)
        {
            _separationWeight = separationWeight;
        }

        public Vector3 CalculateDirection(SteeringContext context)
        {
            Vector3 separation = Vector3.zero;

            foreach (CombatEntity neighbor in context.Neighbors)
            {
                Vector3 difference = context.CurrentPosition - neighbor.CurrentPosition;

                if (difference.magnitude > 0)
                {
                    separation += difference.normalized / difference.magnitude;
                }
            }

            return _separationWeight * separation;
        }
    }
}
