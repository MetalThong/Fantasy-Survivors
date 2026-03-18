namespace Core.Foundation.FSM
{
    public class StateMachineFactory<TContext>
    {
        public static StateMachine<TContext> Create(TContext context, IState<TContext> startState, int capacity)
        {
            StateMachine<TContext> stateMachine = new(context, capacity);
            stateMachine.InitializeState(startState);

            return stateMachine;
        }
    }
}
