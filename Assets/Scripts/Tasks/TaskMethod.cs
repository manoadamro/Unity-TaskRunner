namespace Tasks
{
    public delegate TResponse TaskMethod<TResponse>(out TaskProgress progress);

    public delegate TResponse TaskMethod<TArg1, TResponse>(TArg1 arg1, out TaskProgress progress);

    public delegate TResponse TaskMethod<TArg1, TArg2, TResponse>(
        TArg1 arg1,
        TArg2 arg2,
        out TaskProgress progress);

    public delegate TResponse TaskMethod<TArg1, TArg2, TArg3, TResponse>(
        TArg1 arg1,
        TArg2 arg2,
        TArg3 arg3,
        out TaskProgress progress);
}
