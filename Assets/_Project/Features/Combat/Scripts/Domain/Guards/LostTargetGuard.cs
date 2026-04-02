using Core.Foundation.FSM;

namespace Features.Combat
{
    public class LostTargetGuard : ITransitionGuard<EnemyEntity>
    {
        public bool Evaluate(EnemyEntity context)
        {
            return !context.HasTargetInRange;
        }
    }
}

