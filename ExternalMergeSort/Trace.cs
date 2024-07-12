using System.Diagnostics;

namespace ExternalMergeSort;

public sealed class Trace : ITrace
{
    private readonly List<(string scopeLabel, TimeSpan workTime)> _scopeStats = [];

    public void AddScopeStatistics(string scopeLabel, TimeSpan workTime) =>
        _scopeStats.Add((scopeLabel, workTime));

    public ITraceScope Scope(string type, string method) =>
        new TraceScope(type, method, this);

    public void WriteLine(string line)
    {
        Debug.WriteLine(line);
        Console.WriteLine(line);
    }

    public void WriteStatistics()
    {
        var scopes = _scopeStats
            .GroupBy(
                a => a.scopeLabel,
                a => a.workTime,
                (scopeLabel, workTimes) => new ScopeStat(this, scopeLabel, workTimes))
            .ToList();

        WriteLine("label count min max sum avg");
        foreach (var scope in scopes)
            scope.WriteStatistics();
    }

    private sealed class ScopeStat
    {
        private readonly ITrace _trace;
        private readonly string _label;
        private readonly int _count;
        private readonly TimeSpan _min;
        private readonly TimeSpan _max;
        private readonly TimeSpan _sum;
        private readonly TimeSpan _avg;

        public ScopeStat(ITrace trace, string label, IEnumerable<TimeSpan> workTimes)
        {
            _trace = trace;
            _label = label;
            var times = workTimes.ToArray();
            _count = times.Length;
            _min = times.Min();
            _max = times.Max();
            _sum = new TimeSpan(times.Select(a => a.Ticks).Sum());
            _avg = new TimeSpan(_sum.Ticks / _count);
        }

        public void WriteStatistics()
        {
            _trace.WriteLine($"{_label} {_count} {_min:g} {_max:g} {_sum:g} {_avg:g}");
        }
    }
}