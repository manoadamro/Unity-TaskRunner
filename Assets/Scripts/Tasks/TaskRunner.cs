using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Tasks
{
    [ExecuteInEditMode]
    public class TaskRunner : MonoBehaviour
    {
        public TaskData CurrentTask { get; private set; }

        public bool Busy => CurrentTask != null;

        private List<TaskJoinData> PendingJoinTasks { get; } = new List<TaskJoinData>();

        public void RunTask(TaskData task)
        {
            if (Busy) throw new Exception(); // TODO
            CurrentTask = task;
            var thread = new Thread(TaskThread);
            thread.Start();
        }

        private void TaskThread()
        {
            var task = CurrentTask;
            if (task == null) throw new Exception();

            try { task.Run(); }
            catch (Exception e)
            {
                CurrentTask = null;
                Debug.LogException(e);
                return;
            }

            CurrentTask = null;
            if (task.Join)
            {
                var joinData = task.GetJoinData();
                PendingJoinTasks.Add(joinData);
            }
            else { task.CallCallback(); }
        }

        private void ProcessPendingJoins()
        {
            foreach (var task in PendingJoinTasks.ToArray())
            {
                PendingJoinTasks.Remove(task);
                task.Call();
            }
        }

        public void Update() => ProcessPendingJoins();
    }
}
