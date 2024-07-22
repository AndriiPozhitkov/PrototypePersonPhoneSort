namespace BinaryExternalMergeSort;

public interface ITrace
{
    void AddScopeStatistics(string scopeLabel, TimeSpan workTime);

    ITraceScope Scope(string type, string method);

    void WriteLine(string line);
}