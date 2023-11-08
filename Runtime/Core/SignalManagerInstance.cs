using System;
using System.Collections.Generic;
using UnityEngine;

namespace StinkySteak.Pubsub
{
    internal class SignalManagerInstance
    {
        internal static SignalManagerInstance Instance { get; private set; }

        /// <summary>
        /// Store The List of subscribers that listen to that signal
        /// </summary>
        private readonly Dictionary<Type, List<ISubscription>> _subscription = new();

        internal static void Initialize(SignalManagerInstance instance)
            => Instance = instance;

        internal void Subscribe<T>(Action<T> subscriber, bool oneTime = false) where T : ISignal
        {
            Type signalType = typeof(T);

            if (!_subscription.ContainsKey(signalType))
            {
                _subscription.Add(signalType, new List<ISubscription>());
            }

            _subscription[signalType].Add(new Subscription<T>() { Listener = subscriber.Target, Callback = subscriber, OneTime = oneTime });
        }
        internal void Publish<T>(ISignal signal)
        {
            if (_subscription.TryGetValue(signal.GetType(), out List<ISubscription> subscriptions))
            {
                for (int i = 0; i < subscriptions.Count; i++)
                {
                    ISubscription sub = subscriptions[i];

                    sub.Invoke(signal);

                    if (sub.OneTime)
                        subscriptions.RemoveAt(i);
                }
            }
        }
        internal void Unsubscribe(object listener, Type signal)
        {
            if (_subscription.TryGetValue(signal, out List<ISubscription> subscriptions))
            {
                RemoveSubscription(listener, subscriptions);
                return;
            }

            Debug.LogError($"[SignalManager]: Failed to Unsubscribe, Signal is not recognized yet");
        }

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
            if (Instance == null) return;

            Instance._subscription.Clear();
            Instance = null;
        }
    }
}