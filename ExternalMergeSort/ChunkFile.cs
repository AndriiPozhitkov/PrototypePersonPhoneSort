﻿namespace ExternalMergeSort;

public sealed class ChunkFile : IDisposable
{
    private static readonly EmptyReader EmptyReader = new();

    private readonly FileInfo _file;
    private readonly IReaderFactory _readerFactory;
    private readonly IWriterFactory _writerFactory;
    private readonly Record _record;
    private IReader _reader;
    private bool _canReadNext;

    public ChunkFile(
        FileInfo file,
        IReaderFactory readerFactory,
        IWriterFactory writerFactory)
    {
        _file = file;
        _readerFactory = readerFactory;
        _writerFactory = writerFactory;
        _record = new();
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

    public ChunkFile MinimalRecord(ChunkFile other)
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

    public async Task Write(Chunk chunk)
    {
        using (var writer = _writerFactory.Writer(_file))
        {
            await chunk.Write(writer);
        }

        _reader = _readerFactory.Reader(_file);
    }

    public Task WriteMinimalRecordToOutput(IWriter output)
    {
        _canReadNext = true;
        return _record.Write(output);
    }
}