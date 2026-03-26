using System.Collections;
using System.Collections.Generic;
using Core.Foundation.FSM;
using UnityEngine;

public class CanAttackGuard : ITransitionGuard<EnemyEntity>
{
    public bool Evaluate(EnemyEntity context)
    {
        return context.HasTarget && context.IsCooldownReady && context.IsTargetInRange;
    }
}
