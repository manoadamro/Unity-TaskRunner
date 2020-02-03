using UnityEditor;

namespace Tasks.Editor
{
    [CustomEditor(typeof(TaskRunner))]
    public class ThreadedTaskRunnerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var runner = (TaskRunner) target;
            runner.Update();

            serializedObject.Update();

            var currentTask = runner.Busy ? runner.CurrentTask.Title : "-";
            var description = runner.Busy ? runner.CurrentTask.Progress.Description : "n/a";
            var progress = runner.Busy ? runner.CurrentTask.Progress.Progress : 0f;

            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField($"Current Task: {currentTask} ({progress * 100}%)", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField($"{description}", EditorStyles.label);
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();

            if (runner.Busy)
            {
                EditorUtility.DisplayProgressBar(currentTask, description, progress);
                Repaint();
            }
            else { EditorUtility.ClearProgressBar(); }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
