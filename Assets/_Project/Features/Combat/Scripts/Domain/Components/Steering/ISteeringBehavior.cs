using UnityEngine;

namespace  Features.Combat
{
    public interface ISteeringBehavior
    {
        Vector3 CalculateDirection(SteeringContext context);
    }
}
