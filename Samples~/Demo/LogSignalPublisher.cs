using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace StinkySteak.Pubsub.Sample
{
    public class LogSignalPublisher : MonoBehaviour
    {
        [SerializeField] private string _message;

        private void Start()
        {
            Publish();
        }

        public void Publish()
            => SignalManager.Publish<LogSignal>(new LogSignal() { Message = _message });
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(LogSignalPublisher))]
    public class LogSignalPublisherEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Publish Signal"))
                (target as LogSignalPublisher).Publish();

        }
    }
#endif
}