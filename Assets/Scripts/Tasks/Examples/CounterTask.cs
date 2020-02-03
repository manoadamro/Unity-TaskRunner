using UnityEngine;

namespace Tasks.Examples
{
    [RequireComponent(typeof(TaskRunner))]
    public class CounterTask : MonoBehaviour
    {
        [SerializeField] private int countTo = 1000000;

        public void RunTask()
        {
            var taskRunner = GetComponent<TaskRunner>();
            var task = new TaskData<int, string>
            {
                Title = $"Count to {countTo}",
                Action = MyTask,
                Arg1 = countTo,
                Callback = OnTaskComplete,
                Join = true
            };
            taskRunner.RunTask(task);
        }

        private void OnTaskComplete(string outcome) => Debug.Log(outcome);

        private string MyTask(int maximum, out TaskProgress progress)
        {
            for (var i = 0; i < maximum; i++)
                progress = new TaskProgress(
                    $"Counted to {i + 1} of {maximum}",
                    (i + 1f) / maximum);

            progress = new TaskProgress("Done!", 1f);
            return $"Counted to {countTo}!";
        }
    }
}
