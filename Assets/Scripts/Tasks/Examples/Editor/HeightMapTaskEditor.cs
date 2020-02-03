using UnityEditor;
using UnityEngine;

namespace Tasks.Examples.Editor
{
    [CustomEditor(typeof(HeightMapTask)), CanEditMultipleObjects]
    public class HeightMapTaskEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var task = (HeightMapTask) target;
            if (GUILayout.Button("Run")) task.RunTask();
        }
    }
}
