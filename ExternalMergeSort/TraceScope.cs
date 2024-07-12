using System.Diagnostics;

namespace ExternalMergeSort;

public sealed class TraceScope : ITraceScope
{
    private readonly string _label;
    private readonly ITrace _trace;
    private readonly Stopwatch _workTime;

    public TraceScope(string label, ITrace trace)
    {
        _label = label;
        _trace = trace;
        _trace.WriteLine(label + "...");
        _workTime = Stopwatch.StartNew();
    }

    public void Dispose()
    {
        _workTime.Stop();
        _trace.WriteLine($"{_label} {_workTime.Elapsed:g}");
    }
}