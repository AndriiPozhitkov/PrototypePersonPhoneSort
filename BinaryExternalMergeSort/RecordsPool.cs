namespace BinaryExternalMergeSort;

public sealed class RecordsPool(IRecordsPoolBuffer buffer)
{
    private readonly List<Record> _records = [];
    private int _chunkRecordsCount = 0;

    public async Task ReadChunk(IReader reader)
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
    }

    public void SortChunk() => _records.Sort(0, _chunkRecordsCount, buffer);

    public async Task WriteChunk(IWriter writer)
    {
        for (var i = 0; i < _chunkRecordsCount; i++)
            await _records[i].Write(buffer, writer);
    }
}