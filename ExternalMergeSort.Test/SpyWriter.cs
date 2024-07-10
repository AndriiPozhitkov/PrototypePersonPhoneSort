namespace ExternalMergeSort.Test;

internal sealed class SpyWriter : IWriter
{
    private readonly List<string> _records = [];

    public void Dispose()
    {
    }

    public Task WriteLine(string line)
    {
        _records.Add(line);
        return Task.CompletedTask;
    }

    internal string Records(int i) =>
        _records[i];

    internal int RecordsCount() =>
        _records.Count;
}