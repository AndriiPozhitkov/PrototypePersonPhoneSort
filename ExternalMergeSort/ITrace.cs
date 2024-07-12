namespace ExternalMergeSort;

public interface ITrace
{
    ITraceScope Scope(string type, string method);

    void WriteLine(string line);
}