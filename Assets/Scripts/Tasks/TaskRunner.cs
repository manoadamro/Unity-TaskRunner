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
        
        private List<TaskData> QueuedTasks { get; } = new List<TaskData>();

        public int QueueCount => QueuedTasks.Count;
        
        public void RunTask(TaskData task) => QueuedTasks.Add(task);

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

        private void ProcessNextQueuedTask()
        {
            if(Busy || QueuedTasks.Count == 0) return;
            var task = QueuedTasks[0];
            QueuedTasks.Remove(task);
            
            CurrentTask = task;
            var thread = new Thread(TaskThread);
            thread.Start();
        }

        public void Update()
        {
            ProcessPendingJoins();
            ProcessNextQueuedTask();
        }
    }
}
