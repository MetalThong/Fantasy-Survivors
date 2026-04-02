using Core.Foundation.FSM;

namespace Features.Combat
{
    public class MoveState : StateBase<EnemyEntity>
    {
        public override void FixedUpdate(EnemyEntity context, float fixedDeltaTime)
        {
            context.MoveToTarget(fixedDeltaTime);
        }
    }
}