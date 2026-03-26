using System.Collections;
using System.Collections.Generic;
using Core.Foundation.FSM;
using UnityEngine;

public class IsDeadGuard : ITransitionGuard<EnemyEntity>
{
    public bool Evaluate(EnemyEntity context)
    {
        return context.IsDead;
    }
}