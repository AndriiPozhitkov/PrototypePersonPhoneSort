using System.Diagnostics;

namespace ExternalMergeSort;

public sealed class Trace : ITrace
{
    public ITraceScope Scope(string type, string method) =>
        new TraceScope(type, method, this);

    public void WriteLine(string line)
    {
        Debug.WriteLine(line);
        Console.WriteLine(line);
    }
}