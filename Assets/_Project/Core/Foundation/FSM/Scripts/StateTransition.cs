namespace Core.Foundation.FSM
{
    public class StateTransition<TContext>
    {
        public IState<TContext> From { get; }
        public IState<TContext> To { get; }
        private readonly ITransitionGuard<TContext>[] _guards;

        public StateTransition(IState<TContext> from, IState<TContext> to, params ITransitionGuard<TContext>[] guards)
        {
            From = from;
            To = to;
            _guards = guards;
        }

        public bool CanTransition(TContext context)
        {
            foreach (ITransitionGuard<TContext> guard in _guards)
            {
                if (!guard.Evaluate(context))
                {
                    return false;
                }
            }

            return true;
        }
    }
}