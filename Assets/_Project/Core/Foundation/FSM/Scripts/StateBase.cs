namespace Core.Foundation.FSM
{
    public abstract class StateBase<TContext> : IState<TContext>
    {
        public virtual void Enter(TContext context) {}
        public virtual void Exit(TContext context) {}
        public virtual void Update(TContext context, float deltaTime) {}
        public virtual void FixedUpdate(TContext context, float fixedDeltaTime) {}
    }
}