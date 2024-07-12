namespace ExternalMergeSort;

public sealed class Chunk : IChunk
{
    private static readonly RecordComparer Comparer = new();

    private readonly int _counts;
    private readonly List<Record> _records;

    public Chunk(int counts, List<Record> records)
    {
        _counts = counts;
        _records = records;
    }

    public bool NotEmpty() =>
        _counts > 0;

    public void Sort() =>
        _records.Sort(0, _counts, Comparer);

    public async Task Write(IWriter writer)
    {
        for (var i = 0; i < _counts; i++)
            await _records[i].Write(writer);
    }
}