using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent
{
    private readonly float _speed;
    public Vector3 CurrentPosition { get; private set; }

    public MovementComponent(EnemyStatsSO statsSO)
    {
        _speed = statsSO.Speed;
    }

    public void MovingTo(Vector3 newPosition, float deltaTime)
    {
        Vector3 direction = (newPosition - CurrentPosition).normalized;
        CurrentPosition += _speed * deltaTime * direction;
    }
}
