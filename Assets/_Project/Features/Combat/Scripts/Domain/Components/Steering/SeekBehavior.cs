using UnityEngine;

namespace Features.Combat
{
    public class SeekBehavior : ISteeringBehavior
    {
        public Vector3 CalculateDirection(SteeringContext context)
        {
            return (context.TargetPosition - context.CurrentPosition).normalized;
        }
    }
}

