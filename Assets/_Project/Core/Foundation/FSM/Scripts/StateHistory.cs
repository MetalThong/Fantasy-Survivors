using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Foundation.FSM
{
    public class StateHistory<TContext>
    {
        private readonly int _capacity;
        private readonly Queue<IState<TContext>> _history;
        private readonly List<Type> _stateTypes;

        public IReadOnlyCollection<IState<TContext>> States => _history;
        public IReadOnlyCollection<Type> StateTypes => _stateTypes;

        public StateHistory(int capacity)
        {
            _capacity = capacity;
            _history = new(capacity);
            _stateTypes = new();
        }

        public void Record(IState<TContext> state)
        {
            if (_history.Count >= _capacity)
            {
                _history.Dequeue();
                _stateTypes.RemoveAt(0);
            }

            _history.Enqueue(state);
            _stateTypes.Add(state.GetType());
        }

        public void Reset()
        {
            _history.Clear();
            _stateTypes.Clear();
        }

        public string ToDebugString()
        {
            if (_history.Count == 0) 
            {
                return "<empty>"; 
            }

            StringBuilder sb = new();

            foreach (var state in _history)
            {
                if (sb.Length > 0) sb.Append(" -> ");
                
                sb.Append(state.GetType().Name);
            }

            return sb.ToString();
        }
    }
}
