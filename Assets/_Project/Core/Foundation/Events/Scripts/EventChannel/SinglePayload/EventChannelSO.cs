using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

namespace Core.Foundation.Events
{
    public abstract class EventChannelSO<T> : ScriptableObject
    {
        private readonly List<EventListener<T>> _eventRaiseds = new();
        private bool _isRunning;

        public void RaiseEvent(T value)
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
                List<EventListener<T>> listeners = new(_eventRaiseds);
                foreach (EventListener<T> listener in listeners)
                {
                    try
                    {
                        listener.Raise(value);
                    }
                    catch (Exception exception)
                    {
                        Debug.Log($"RaiseEvent called but exception: {exception.Message}");
                    }
                }
            }
            finally
            {
                _isRunning = false;
            }
        }

        public void AddListener(EventListener<T> listener)
        {
            if (listener == null)
            {
                Debug.Log("AddListener called but no listeners are registered.");
                return;
            }
            if (!_eventRaiseds.Contains(listener))
            {
                _eventRaiseds.Add(listener);
            }
        }

        public void RemoveListener(EventListener<T> listener)
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

