namespace ExternalMergeSort;

public interface ITrace
{
    ITraceScope Scope(string label);

    void WriteLine(string line);
}

public interface ITraceScope : IDisposable
{
}