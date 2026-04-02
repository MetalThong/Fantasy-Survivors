using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Foundation.Events
{
    public abstract class EventChannelSO<T1, T2> : ScriptableObject
    {
        private readonly List<EventListener<T1, T2>> _eventRaiseds = new();
        private bool _isRunning;
        
        public void EventRaise(T1 value1, T2 value2)
        {
            if (_isRunning)
            {
                Debug.Log("RaiseEvent called while event is already running (reentrancy blocked).");
                return;
            }
            try
            {
                if (_eventRaiseds == null)
                {
                    Debug.Log("RaiseEvent called but no listeners are registered.");
                    return;
                }

                _isRunning = true;
                List<EventListener<T1, T2>> listeners = new(_eventRaiseds);
                foreach (EventListener<T1, T2> listener in listeners)
                {
                    try
                    {
                        listener.Raise(value1, value2);
                    }
                    catch (Exception exception)
                    {
                        Debug.Log($"RaiseEvent called but exeption {exception.Message}");
                    }
                }
            }
            finally
            {
                _isRunning = false;
            }
        }

        public void AddListener(EventListener<T1, T2> listener)
        {
            if (listener == null)
            {
                Debug.Log("AddListener called but no listeners are registered.");
                return;
            }
            if (!_eventRaiseds.Contains(listener))
            {
                _eventRaiseds.Add(listener);
                Debug.Log("Listener added: " + listener);
            }
        }

        public void RemoveListener(EventListener<T1, T2> listener)
        {
            if (listener == null)
            {
                Debug.Log("RemoveListener called but no listeners are registered.");
                return;
            }
            if (_eventRaiseds.Contains(listener))
            {
                Debug.Log("Listener removed: " + listener);
                _eventRaiseds.Remove(listener);
            }
        }
    }
}
