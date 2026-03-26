using System.Collections;
using System.Collections.Generic;
using Core.Foundation.FSM;
using UnityEngine;

public class IsAttackFinishedGuard : ITransitionGuard<EnemyEntity>
{
    public bool Evaluate(EnemyEntity context)
    {
        return context.IsAttackFinished;
    }
}
