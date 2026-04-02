using Core.Foundation.FSM;

namespace Features.Combat
{
    public class StateChangedPayload
    {
        public CombatEntity Source;
        public IState<EnemyEntity> CurrentState;
        public string EntityAnimation;

        public StateChangedPayload(CombatEntity source, IState<EnemyEntity> currentState, string entityAnimation)
        {
            Source = source;
            CurrentState = currentState;
            EntityAnimation = entityAnimation;
        }
    }
}