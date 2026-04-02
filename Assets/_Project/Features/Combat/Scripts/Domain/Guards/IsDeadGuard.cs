using Core.Foundation.FSM;

namespace Features.Combat
{
    public class IsDeadGuard : ITransitionGuard<EnemyEntity>
    {
        public bool Evaluate(EnemyEntity context)
        {
            return !context.IsAlive && context.CurrentState is not DieState;
        }
    }
}

