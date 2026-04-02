namespace Core.Foundation.FSM
{
    public class StateMachineFactory<TContext>
    {
        public static StateMachine<TContext> Create(TContext context, IState<TContext> initialState, int capacity = 5)
        {
            StateMachine<TContext> stateMachine = new(context, capacity);
            stateMachine.InitializeState(initialState);

            return stateMachine;
        }
    }
}
