using System.Diagnostics;

namespace ExternalMergeSort;

public sealed class Trace : ITrace
{
    public ITraceScope Scope(string label) =>
        new TraceScope(label, this);

    public void WriteLine(string line)
    {
        Debug.WriteLine(line);
        Console.WriteLine(line);
    }
}