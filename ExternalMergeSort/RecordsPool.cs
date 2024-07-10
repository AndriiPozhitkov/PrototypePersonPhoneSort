namespace ExternalMergeSort;

public sealed class RecordsPool
{
    private readonly List<Record> _records;
    private readonly IRecordsPoolSize _size;

    public RecordsPool(IRecordsPoolSize size)
    {
        _size = size;
        _records = _size.Records();
    }

    public async Task<Chunk> ReadChunk(IReader reader)
    {
        var count = 0;

        foreach (var record in _records)
        {
            var readed = await record.TryRead(reader);

            if (!readed)
                break;

            count++;
        }

        return new(count, _records);
    }
}