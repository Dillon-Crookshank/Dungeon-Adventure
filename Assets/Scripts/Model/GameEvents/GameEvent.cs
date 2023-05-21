using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace {
    [CreateAssetMenu(menuName = "Game Event")]
    public class GameEvent : ScriptableObject
    {
        public List<GameEventListener> listeners = new List<GameEventListener>();

        public void Raise(Component sender, DataPacket data)
        {
            foreach (GameEventListener listener in listeners)
            {
                if (
                    listener != sender
                    && (data.GetDestination() == null || data.GetDestination().Equals(listener.name))
                )
                    listener.OnEventRaised(sender, data);
            }
        }

        public void RegisterListener(GameEventListener listener)
        {
            if (!listeners.Contains(listener))
            {
                listeners.Add(listener);
            }
        }

        public void DeregisterListener(GameEventListener listener)
        {
            if (listeners.Contains(listener))
            {
                listeners.Remove(listener);
            }
        }
    }
}
