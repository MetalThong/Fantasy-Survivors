namespace Core.Foundation.FSM
{
    public interface IState<TContext>
    {
        void Enter(TContext context);
        void Exit(TContext context);
        void Update(TContext context, float deltaTime);
        void FixedUpdate(TContext context, float fixedDeltaTime);
    }
}