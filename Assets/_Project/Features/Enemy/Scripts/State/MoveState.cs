using System.Collections;
using System.Collections.Generic;
using Core.Foundation.FSM;
using UnityEngine;

public class MoveState : StateBase<EnemyEntity>
{
    public override void Enter(EnemyEntity context)
    {
        
    }

    public override void Update(EnemyEntity context, float deltaTime)
    {
        context.MovingToTarget(deltaTime);
    }

    public override void Exit(EnemyEntity context)
    {
        
    }
}
