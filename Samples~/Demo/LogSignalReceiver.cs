using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace StinkySteak.Pubsub.Sample
{
    public class LogSignalReceiver : MonoBehaviour
    {
        [SerializeField] private bool _listenOnlyOnce;

        public bool ListenOnlyOnce => _listenOnlyOnce;

        private void Start()
            => SignalManager.Subscribe<LogSignal>(OnSignalReceived, _listenOnlyOnce);

        public void OnSignalReceived(LogSignal logSignal)
            => Debug.Log($"Log: {logSignal.Message}");
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(LogSignalReceiver))]
    public class LogSignalReceiverEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LogSignalReceiver signalReceiver = target as LogSignalReceiver;

            if(GUILayout.Button("Unsubscribe"))
            {
                SignalManager.Unsubscribe(target, typeof(LogSignal));
            }

            if(GUILayout.Button("Subscribe"))
            {
                SignalManager.Subscribe<LogSignal>(signalReceiver.OnSignalReceived, signalReceiver.ListenOnlyOnce);
            }
        }
    }
#endif
}