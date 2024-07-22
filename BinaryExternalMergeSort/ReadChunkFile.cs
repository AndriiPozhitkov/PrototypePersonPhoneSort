namespace BinaryExternalMergeSort;

public sealed class ReadChunkFile(
        IRecordsPoolBuffer buffer,
        FileInfo file,
        int number,
        IReader reader) :
    IDisposable
{
    private readonly ChunkFileRecord _record = new(buffer, number);

    private bool _canReadNext = true;

    public void Dispose()
    {
        reader.Dispose();

        file.Refresh();
        if (file.Exists)
        {
            file.Delete();
        }
    }

    public bool IsReaded() =>
        _record.IsReaded();

    public ReadChunkFile MinimalRecord(ReadChunkFile other)
    {
        var isReaded = IsReaded();
        if (isReaded && other.IsReaded())
        {
            return _record.CompareTo(other._record) <= 0 ? this : other;
        }
        else if (isReaded)
        {
            return this;
        }
        else
        {
            return other;
        }
    }

    public async Task ReadNextRecord()
    {
        if (_canReadNext)
        {
            _canReadNext = false;
            await _record.TryRead(reader);
        }
    }

    public Task WriteMinimalRecordToOutput(IWriter output)
    {
        _canReadNext = true;
        return _record.Write(output);
    }
}