namespace BinaryExternalMergeSort;

public sealed class ChunkFileFactory : IChunkFileFactory
{
    private readonly IRecordsPoolBufferFactory _bufferFactory;
    private readonly IReaderFactory _readerFactory;
    private readonly ITempFileFactory _tempFileFactory;
    private readonly IWriterFactory _writerFactory;

    public ChunkFileFactory(
        IRecordsPoolBufferFactory bufferFactory,
        IReaderFactory readerFactory,
        ITempFileFactory tempFileFactory,
        IWriterFactory writerFactory)
    {
        _bufferFactory = bufferFactory;
        _readerFactory = readerFactory;
        _tempFileFactory = tempFileFactory;
        _writerFactory = writerFactory;
    }

    public void ReadChunkFiles(
        List<WriteChunkFile> writes,
        List<ReadChunkFile> reads)
    {
        foreach (var write in writes)
        {
            var buffer = _bufferFactory.ChunkFileBuffer(writes.Count);
            var read = write.ReadChunkFile(buffer, _readerFactory);
            reads.Add(read);
        }

        writes.Clear();
        writes.TrimExcess();
    }

    public WriteChunkFile WriteChunkFile()
    {
        var file = _tempFileFactory.TempFile();
        return new(file, _writerFactory);
    }
}