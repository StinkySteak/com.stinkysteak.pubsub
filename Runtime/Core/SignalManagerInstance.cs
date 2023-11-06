using System;
using System.Collections.Generic;
using UnityEngine;

namespace StinkySteak.Pubsub
{
    internal class SignalManagerInstance : BaseSignalManager
    {
        public static SignalManagerInstance Instance { get; private set; }

        private void RemoveSubscription(object listener, List<ISubscription> subscriptions)
        {
            for (int i = 0; i < subscriptions.Count; i++)
            {
                if (subscriptions[i].Listener == listener)
                {
                    subscriptions.RemoveAt(i);
                    return;
                }
            }

            Debug.LogWarning($"[SignalManager]: Failed to Unsubscribe listener: {listener}!");
        }

        static internal void Dispose()
        {
            Instance = null;
        }
    }
}