using System.Collections.Generic;

namespace Core.Foundation.FSM
{
    public class StateHistory<TContext>
    {
        private readonly Queue<IState<TContext>> _history;
        private readonly int _capacity;

        public StateHistory(int capacity)
        {
            _history = new(capacity);
        }

        public void Record(IState<TContext> state)
        {
            if (_history.Count >= _capacity)
            {
                _history.Dequeue();
            }

            _history.Enqueue(state);
        }
    }
}
