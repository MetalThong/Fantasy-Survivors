using System;
using System.Collections.Generic;

namespace Core.Foundation.FSM
{
    public class StateMachine<TContext> : IStateMachine<TContext>
    {
        public IState<TContext> CurrentState { get; private set; }
        public StateHistory<TContext> History { get; private set; }
        private readonly List<StateTransition<TContext>> _transitions;
        private readonly TContext _context;

        public event Action<IState<TContext>> OnStateEntered;
        public event Action<IState<TContext>> OnStateExited;

        public StateMachine(TContext context, int capacity)
        {
            _context = context;
            _transitions = new();

            History = new(capacity);
        }

        public void InitializeState(IState<TContext> startState)
        {
            CurrentState = startState;

            CurrentState.Enter(_context);
            OnStateEntered?.Invoke(CurrentState);
            
            History.Record(CurrentState);
        }

        public void ChangeState(IState<TContext> newState)
        {
            CurrentState?.Exit(_context);
            OnStateExited?.Invoke(CurrentState);

            CurrentState = newState;

            CurrentState.Enter(_context);
            OnStateEntered?.Invoke(CurrentState);
            
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
    }
}