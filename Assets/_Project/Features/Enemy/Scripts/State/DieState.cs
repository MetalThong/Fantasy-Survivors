using System.Collections;
using System.Collections.Generic;
using Core.Foundation.FSM;
using UnityEngine;

public class DieState : StateBase<EnemyEntity>
{
    public override void Enter(EnemyEntity context)
    {
        
    }

    public override void Update(EnemyEntity context, float deltaTime)
    {
        context.OnDead();
    }

    public override void Exit(EnemyEntity context)
    {
        
    }
}