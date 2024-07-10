namespace ExternalMergeSort;

public sealed class Record : IComparable<Record>
{
    private string _line = "";
    private bool _isReaded;

    public Record()
    {
    }

    public Record(string line) =>
        Split(line);

    public int CompareTo(Record? y) =>
        y == null ? 1 : _line.CompareTo(y._line);

    public bool Equals(string line) =>
        _line == line;

    public bool IsReaded() =>
        _isReaded;

    public async Task<bool> TryRead(IReader reader)
    {
        var line = await reader.ReadLine();
        _isReaded = TrySplit(line);
        return _isReaded;
    }

    private bool TrySplit(string? line) =>
        line != null && Split(line);

    private bool Split(string line)
    {
        _line = line;
        return true;
    }

    public Task Write(IWriter writer) =>
        writer.WriteLine(_line);
}