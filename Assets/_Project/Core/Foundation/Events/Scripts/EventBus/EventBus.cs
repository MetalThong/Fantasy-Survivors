using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Foundation.Events
{
    public class EventBus : IEventBus, IDisposable
    {
        private readonly Dictionary<Type, List<Delegate>> _payload2Handlers = new();

        public void Subscribe<T>(Action<T> handler)
        {
            Type type = typeof(T);
            if (!_payload2Handlers.ContainsKey(type))
            {
                _payload2Handlers[type] = new List<Delegate>();
            }
                
            _payload2Handlers[type].Add(handler);
        }

        public void Unsubscribe<T>(Action<T> handler)
        {
            Type type = typeof(T);
            if (_payload2Handlers.TryGetValue(type, out var list))
            {
                list.Remove(handler);
            }
        }

        public void Publish<T>(T eventData)
        {
            Type type = typeof(T);
            if (!_payload2Handlers.TryGetValue(type, out var list)) 
            {
                return;
            }

            List<Delegate> copy = new(list);
            foreach (var handler in copy)
            {
                try
                {
                    ((Action<T>)handler)?.Invoke(eventData);
                }
                catch (Exception exception)
                {
                    Debug.LogError($"[EventBus] Handler error for {type.Name}: {exception}");
                }
            }
        }

        public void Dispose()
        {
            _payload2Handlers.Clear();
        }
    }
}
