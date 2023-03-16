using UnityEngine;

namespace StinkySteak.Pubsub
{
    public class Subscription<T> : ISubscription where T : ISignal
    {
        public object Listener;
        public System.Action<T> Callback;
        public bool OneTime;

        object ISubscription.Listener => Listener;

        bool ISubscription.OneTime => OneTime;

        /// <summary>
        /// Anticipating, Listener can be null but cannot be checked by <br/>
        /// Listener == null
        /// </summary>
        /// <returns></returns>
        private bool IsListenerNull()
        {
            if(Listener is MonoBehaviour behaviour)
            {
                return behaviour == null;
            }

            return false;
        }

        public void Invoke(ISignal signal)
        {
            if (IsListenerNull())
            {
                SignalManager.Unsubscribe(Listener, typeof(T));
                return;
            }

            Callback?.Invoke((T)signal);
        }
    }
}