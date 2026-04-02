using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Foundation.FSM
{
    public class StateMachine<TContext> : IStateMachine<TContext>
    {
        public IState<TContext> CurrentState { get; private set; }
        public StateHistory<TContext> History { get; private set; }
        private IState<TContext> _initialState;
        private readonly List<StateTransition<TContext>> _transitions;
        private readonly TContext _context;

        public event Action OnStateChanged;

        public StateMachine(TContext context, int capacity = 5)
        {
            _context = context;
            _transitions = new();

            History = new(capacity);
        }

        public void InitializeState(IState<TContext> initialState)
        {
            _initialState = initialState;
            CurrentState = initialState;

            CurrentState.Enter(_context);
            OnStateChanged?.Invoke();
            
            History.Record(CurrentState);
        }

        public void ChangeState(IState<TContext> newState)
        {
            CurrentState?.Exit(_context);
            
            CurrentState = newState;
            OnStateChanged?.Invoke();

            CurrentState.Enter(_context);            
            History.Record(CurrentState);
        }

        public void Update(float deltaTime)
        {
            CurrentState?.Update(_context, deltaTime);
            TryChangeState();
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            CurrentState?.FixedUpdate(_context, fixedDeltaTime);
        }

        private void TryChangeState()
        {
            if (CurrentState == null)
            {
                return;
            }

            foreach (StateTransition<TContext> transition in _transitions)
            {
                if (transition.From == null || transition.From == CurrentState)
                {
                    if (transition.CanTransition(_context))
                    {
                        ChangeState(transition.To);
                        break;
                    }
                }
            }
        }

        public void AddTransition(IState<TContext> from, IState<TContext> to, params ITransitionGuard<TContext>[] guards)
        {
            _transitions.Add(new StateTransition<TContext>(from, to, guards));
        }
        
        public void Reset()
        {
            History.Reset();
            InitializeState(_initialState);
        }

        #if UNITY_EDITOR
        public string GetTransitionsDebugString()
        {
            if (_transitions == null || _transitions.Count == 0)
            {
                return "<No Transitions>";
            }

            StringBuilder sb = new();

            foreach (var transition in _transitions)
            {
                sb.AppendLine(transition.ToDebugString(_context));
            }

            return sb.ToString();
        }
        #endif
    }
}