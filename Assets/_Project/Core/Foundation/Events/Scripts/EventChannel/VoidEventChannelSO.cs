using UnityEngine;
using System;

namespace Core.Foundation.Events
{
    [CreateAssetMenu(fileName = "SO_VoidEventChannel", menuName = "SO/Core/Event/VoidEventChannel")]
    public class VoidEventChannelSO : ScriptableObject
    {
        public delegate void OnHandler();
        private event OnHandler EventRaised;
        private bool _isRunning;

        public void RaiseEvent()
        {
            if (_isRunning)
            {
                Debug.Log("RaiseEvent called while event is already running (reentrancy blocked).");
                return;
            }

            try
            {
                if (EventRaised == null)
                {
                    Debug.Log("RaiseEvent called but no listeners are registered.");
                    return;
                }

                _isRunning = true;
                Delegate[] listeners = EventRaised.GetInvocationList();
                foreach (Delegate listener in listeners)
                {
                    try
                    {
                        ((OnHandler)listener)?.Invoke();
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
    }   
}
