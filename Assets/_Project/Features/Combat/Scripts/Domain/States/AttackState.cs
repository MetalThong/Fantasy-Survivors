using Core.Foundation.FSM;

namespace Features.Combat
{
    public class AttackState : StateBase<EnemyEntity>
    {
        public override void Enter(EnemyEntity context)
        {
            context.TryExecuteAttack();
        }
    }
}