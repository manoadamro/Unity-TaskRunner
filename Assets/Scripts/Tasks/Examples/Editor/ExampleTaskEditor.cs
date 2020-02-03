using UnityEditor;
using UnityEngine;

namespace Tasks.Examples.Editor
{
    [CustomEditor(typeof(CounterTask)), CanEditMultipleObjects]
    public class ExampleTaskEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var task = (CounterTask) target;
            if (GUILayout.Button("Run")) task.RunTask();
        }
    }
}
