namespace BinaryExternalMergeSort;

public sealed class RecordsPool(IRecordsPoolBuffer buffer) : IRecordsPool
{
    private readonly List<Record> _records = [];
    private int _chunkRecordsCount = 0;

    public bool NotEmpty() => _chunkRecordsCount > 0;

    public async Task<IChunk> ReadChunk(IReader reader)
    {
        if (await buffer.Read(reader))
        {
            var i = 0;
            while (buffer.ScanNextRecord())
            {
                if (i < _records.Count)
                {
                    _records[i].SetBegin(buffer);
                }
                else
                {
                    _records.Add(new(buffer));
                }
                i++;
            }
            _chunkRecordsCount = i;
        }
        else
        {
            _chunkRecordsCount = 0;
        }
        // TODO _records.TrimExcess(); ?
        return this;
    }

    public void Sort() => _records.Sort(0, _chunkRecordsCount, buffer);

    public async Task Write(IWriter writer)
    {
        for (var i = 0; i < _chunkRecordsCount; i++)
            await buffer.Write(_records[i], writer);
    }
}