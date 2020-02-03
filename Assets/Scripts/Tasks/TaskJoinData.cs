using System;

namespace Tasks
{
    public abstract class TaskJoinData
    {
        public abstract void Call();
    }

    public class TaskJoinData<TArg> : TaskJoinData
    {
        public TArg Arg;
        public Action<TArg> Callback;

        public TaskJoinData(Action<TArg> callback, TArg arg)
        {
            Callback = callback;
            Arg = arg;
        }

        public override void Call() => Callback(Arg);
    }
}
