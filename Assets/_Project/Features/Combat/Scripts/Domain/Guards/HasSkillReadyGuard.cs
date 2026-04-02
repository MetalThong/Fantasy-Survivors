using Core.Foundation.FSM;

namespace Features.Combat
{
    public class HasSkillReadyGuard : ITransitionGuard<EnemyEntity>
    {
        public bool Evaluate(EnemyEntity context)
        {
            return context.HasSkillReady;
        }
    }
}

