using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Asteroids
{
    public static class EventManager
    {
        private static readonly Dictionary<string, UnityEvent<object>> Events = new Dictionary<string, UnityEvent<object>>();

        public static void StartListening(string eventName, UnityAction<object> callback)
        {
            if (Events.TryGetValue(eventName, out var thisEvent))
            {
                thisEvent.AddListener(callback);
            }
            else
            {
                thisEvent = new UnityEvent<object>();
                thisEvent.AddListener(callback);
                Events.Add(eventName, thisEvent);
            }
        }
        
        public static void StopListening(string eventName, UnityAction<object> callback)
        {
            if (Events.TryGetValue(eventName, out var thisEvent))
            {
                thisEvent.RemoveListener(callback);
            }
        }
        
        public static void StopListeningAll(string eventName)
        {
            if (Events.TryGetValue(eventName, out var thisEvent))
            {
                thisEvent.RemoveAllListeners();
            }
        }

        public static void EmitEvent(string eventName, object data)
        {
            if (Events.TryGetValue(eventName, out var thisEvent))
            {
                thisEvent.Invoke(data);
            }
        }
    }
}