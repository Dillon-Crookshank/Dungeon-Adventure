using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DungeonAdventure
{
    [System.Serializable]
    public class CustomGameEvent : UnityEvent<Component, object> { }

    public class GameEventListener : MonoBehaviour
    {
        public GameEvent gameEvent;

        public CustomGameEvent response;

        public void OnEnable()
        {
            gameEvent.RegisterListener(this);
        }

        public void OnDisable()
        {
            gameEvent.DeregisterListener(this);
        }

        public void OnEventRaised(Component sender, object data)
        {
            response.Invoke(sender, data);
        }
    }
}