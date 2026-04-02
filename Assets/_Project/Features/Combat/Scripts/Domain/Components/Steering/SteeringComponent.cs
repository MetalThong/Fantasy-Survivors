using System.Collections.Generic;
using UnityEngine;

namespace Features.Combat
{
    public class SteeringComponent
    {
        private readonly List<ISteeringBehavior> _steeringBehaviors;

        public SteeringComponent(params ISteeringBehavior[] steeringBehaviors)
        {
            _steeringBehaviors = new(steeringBehaviors);       
        }

        public Vector3 CalculateTotalDirection(SteeringContext context)
        {
            Vector3 direction = Vector3.zero;

            foreach (ISteeringBehavior steeringBehavior in _steeringBehaviors)
            {
                direction += steeringBehavior.CalculateDirection(context);
            }

            return direction;
        }
    }  
}