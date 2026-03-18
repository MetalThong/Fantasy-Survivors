namespace Core.Foundation.FSM
{
    public interface ITransitionGuard<TContext>
    {
        bool Evaluate(TContext context);
    }
}
