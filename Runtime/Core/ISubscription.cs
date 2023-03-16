using UnityEngine;

namespace StinkySteak.Pubsub
{
    public interface ISubscription
    {
        object Listener { get; }
        void Invoke(ISignal signal);
        bool OneTime { get; }
    }
}