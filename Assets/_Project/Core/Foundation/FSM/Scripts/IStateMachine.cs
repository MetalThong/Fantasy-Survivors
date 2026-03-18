using System.Collections;
namespace Core.Foundation.FSM
{
    public interface IStateMachine<TContext>
    {
        IState<TContext> CurrentState { get; }
        void InitializeState(IState<TContext> startState);
        void ChangeState(IState<TContext> newState);
        void Update(float deltaTime);
        void FixedUpdate(float fixedDeltaTime);
    }
}

