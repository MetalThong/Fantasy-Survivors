using System.Collections;
using System.Collections.Generic;
using Core.Foundation.FSM;
using UnityEditorInternal;
using UnityEngine;

public class AttackState : StateBase<EnemyEntity>
{
    public override void Enter(EnemyEntity context)
    {
        context.StartAttackDuration();
    }

    public override void Update(EnemyEntity context, float deltaTime)
    {
        context.Tick(deltaTime);
    }

    public override void Exit(EnemyEntity context)
    {
        context.StartCooldown();
    }
}
