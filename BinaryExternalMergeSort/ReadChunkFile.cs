namespace BinaryExternalMergeSort;

public sealed class ReadChunkFile : IDisposable
{
    private readonly FileInfo _file;
    private readonly IReader _reader;
    private readonly ChunkFileRecord _record;

    private bool _canReadNext;

    public ReadChunkFile(
        IRecordsPoolBuffer buffer,
        FileInfo file,
        IReader reader)
    {
        _file = file;
        _reader = reader;
        _record = new(buffer);
        _canReadNext = true;
    }

    public void Dispose()
    {
        _reader.Dispose();

        _file.Refresh();
        if (_file.Exists)
        {
            _file.Delete();
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
            await _record.TryRead(_reader);
        }
    }

    public Task WriteMinimalRecordToOutput(IWriter output)
    {
        _canReadNext = true;
        return _record.Write(output);
    }
}