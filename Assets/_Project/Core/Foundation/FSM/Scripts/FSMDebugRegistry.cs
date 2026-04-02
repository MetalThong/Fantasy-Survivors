using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Core.Foundation.FSM
{
    public class FSMDebugRegistry
    {
        private static readonly Dictionary<Type, List<IFsmDebugView>> _contextType2DebugViews = new();
        public static IReadOnlyDictionary<Type, List<IFsmDebugView>> ContextType2DebugViews => _contextType2DebugViews;

        public static void Register(IFsmDebugView fsmDebugView)
        {
            if (!_contextType2DebugViews.TryGetValue(fsmDebugView.ContextType, out var list))
            {
                list = new List<IFsmDebugView>();
                _contextType2DebugViews.Add(fsmDebugView.ContextType, list);
            }

            list.Add(fsmDebugView);
        }
        

        public static void UnRegister(IFsmDebugView fsmDebugView)
        {
            if (!_contextType2DebugViews.TryGetValue(fsmDebugView.ContextType, out var list))
            {
                return;
            }

            list.Remove(fsmDebugView);

            if (list.Count == 0)
            {
                _contextType2DebugViews.Remove(fsmDebugView.ContextType);
            }
        }
    }
}
