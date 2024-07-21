namespace BinaryExternalMergeSort;

public sealed class ReadChunkFile : IDisposable
{
    private static readonly EmptyReader EmptyReader = new();

    private readonly FileInfo _file;
    private readonly IReaderFactory _readerFactory;
    private readonly ChunkFileRecord _record;

    private bool _canReadNext;
    private IReader _reader;

    public ReadChunkFile(
        IRecordsPoolBuffer buffer,
        FileInfo file,
        IReaderFactory readerFactory)
    {
        _file = file;
        _readerFactory = readerFactory;
        _record = new(buffer);
        _reader = EmptyReader;
        _canReadNext = true;
    }

    public void Dispose()
    {
        _reader.Dispose();
        _reader = EmptyReader;

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