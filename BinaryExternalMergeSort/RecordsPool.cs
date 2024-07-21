namespace BinaryExternalMergeSort;

public sealed class RecordsPool(IRecordsPoolBuffer buffer) : IRecordsPool
{
    private readonly IRecordsPoolBuffer _buffer = buffer;
    private readonly List<Record> _records = [];
    private int _chunkRecordsCount = 0;

    public void Dispose()
    {
        _buffer.Dispose();
        _records.Clear();
        _records.TrimExcess();
    }

    public bool NotEmpty() => _chunkRecordsCount > 0;

    public async Task<IChunk> ReadChunk(IReader reader)
    {
        if (await _buffer.Read(reader))
        {
            var i = 0;
            while (_buffer.ScanNextRecord())
            {
                if (i < _records.Count)
                {
                    _records[i].SetBegin(_buffer);
                }
                else
                {
                    _records.Add(new(_buffer));
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

    public void Sort() => _records.Sort(0, _chunkRecordsCount, _buffer);

    public async Task Write(IWriter writer)
    {
        for (var i = 0; i < _chunkRecordsCount; i++)
            await _buffer.Write(_records[i], writer);
    }
}