using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Asteroids
{
    public static class EventManager
    {
        private static readonly Dictionary<string, UnityEvent<object>> _events = new Dictionary<string, UnityEvent<object>>();

        public static void StartListening(string eventName, UnityAction<object> callback)
        {
            if (_events.TryGetValue(eventName, out var thisEvent))
            {
                thisEvent.AddListener(callback);
            }
            else
            {
                thisEvent = new UnityEvent<object>();
                thisEvent.AddListener(callback);
                _events.Add(eventName, thisEvent);
            }
        }
        
        public static void StopListening(string eventName, UnityAction<object> callback)
        {
            if (_events.TryGetValue(eventName, out var thisEvent))
            {
                thisEvent.RemoveListener(callback);
            }
        }
        
        public static void StopListeningAll(string eventName)
        {
            if (_events.TryGetValue(eventName, out var thisEvent))
            {
                thisEvent.RemoveAllListeners();
            }
        }

        public static void EmitEvent(string eventName, object data)
        {
            if (_events.TryGetValue(eventName, out var thisEvent))
            {
                thisEvent.Invoke(data);
            }
        }
    }
}