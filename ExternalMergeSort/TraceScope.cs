using System.Diagnostics;

namespace ExternalMergeSort;

public sealed class TraceScope : ITraceScope
{
    private readonly string _label;
    private readonly ITrace _trace;
    private readonly Stopwatch _workTime;

    public TraceScope(string type, string method, ITrace trace)
    {
        _label = type + "." + method;
        _trace = trace;
        _trace.WriteLine($"{_label}...");
        _workTime = Stopwatch.StartNew();
    }

    public void Dispose()
    {
        _workTime.Stop();
        _trace.WriteLine($"{_label} {_workTime.Elapsed:g}");
        _trace.AddScopeStatistics(_label, _workTime.Elapsed);
    }
}