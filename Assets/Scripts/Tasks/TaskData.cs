using System;

namespace Tasks
{
    public abstract class TaskData
    {
        public bool Join;

        public TaskProgress Progress = new TaskProgress {Progress = 0f, Description = "-"};

        public string Title;

        public abstract void Run();

        public abstract void CallCallback();

        public abstract TaskJoinData GetJoinData();
    }

    public class TaskData<TResponse> : TaskData
    {
        public TaskMethod<TResponse> Action;
        public Action<TResponse> Callback;
        public TResponse Response;

        public override void Run() => Response = Action(out Progress);

        public override void CallCallback() => Callback(Response);

        public override TaskJoinData GetJoinData() => new TaskJoinData<TResponse>(Callback, Response);
    }

    public class TaskData<TArg1, TResponse> : TaskData
    {
        public TaskMethod<TArg1, TResponse> Action;
        public TArg1 Arg1;
        public Action<TResponse> Callback;
        public TResponse Response;

        public override void Run() => Response = Action(Arg1, out Progress);

        public override void CallCallback() => Callback(Response);

        public override TaskJoinData GetJoinData() => new TaskJoinData<TResponse>(Callback, Response);
    }

    public class TaskData<TArg1, TArg2, TResponse> : TaskData
    {
        public TaskMethod<TArg1, TArg2, TResponse> Action;
        public TArg1 Arg1;
        public TArg2 Arg2;
        public Action<TResponse> Callback;
        public TResponse Response;

        public override void Run() => Response = Action(Arg1, Arg2, out Progress);

        public override void CallCallback() => Callback(Response);

        public override TaskJoinData GetJoinData() => new TaskJoinData<TResponse>(Callback, Response);
    }

    public class TaskData<TArg1, TArg2, TArg3, TResponse> : TaskData
    {
        public TaskMethod<TArg1, TArg2, TArg3, TResponse> Action;
        public TArg1 Arg1;
        public TArg2 Arg2;
        public TArg3 Arg3;
        public Action<TResponse> Callback;
        public TResponse Response;

        public override void Run() => Response = Action(Arg1, Arg2, Arg3, out Progress);

        public override void CallCallback() => Callback(Response);

        public override TaskJoinData GetJoinData() => new TaskJoinData<TResponse>(Callback, Response);
    }
}
