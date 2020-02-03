namespace Tasks
{
    public struct TaskProgress
    {
        public float Progress;
        public string Description;

        public TaskProgress(string description, float progress)
        {
            Description = description;
            Progress = progress;
        }
    }
}
