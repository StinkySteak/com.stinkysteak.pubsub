using System;

namespace StinkySteak.Pubsub
{
    public static class SignalManager
    {
        public static void Subscribe<T>(Action<T> subscriber, bool oneTime = false) where T : ISignal
            => SignalManagerInstance.Instance.Subscribe(subscriber, oneTime);

        public static void Publish<T>(ISignal signal)
           => SignalManagerInstance.Instance.Publish<T>(signal);

        public static void Unsubscribe(object listener, Type signal)
            => SignalManagerInstance.Instance.Unsubscribe(listener, signal);

        public static void Dispose()
        {
            SignalManagerInstance.Dispose();
        }
    }
}