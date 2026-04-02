using System.Text;

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

        #if UNITY_EDITOR
        public string ToDebugString(TContext context)
        {
            string from = From?.GetType().Name ?? "Any";
            string to = To?.GetType().Name   ?? "<None>";

            if (_guards == null || _guards.Length == 0)
            {
                return $"{from} -> {to} (No Guards)";
            }

            StringBuilder sb = new();
            sb.AppendLine($"{from} -> {to}");

            foreach (var guard in _guards)
            {
                bool result = guard.Evaluate(context);
                sb.AppendLine($"  └─ [{(result ? "✓" : "✗")}] {guard.GetType().Name}");
            }

            return sb.ToString();
        }
        #endif
    }
}