namespace ExternalMergeSort;

public sealed class ChunkFileFactory : IChunkFileFactory
{
    private readonly ITempFileFactory _tempFileFactory;
    private readonly IReaderFactory _readerFactory;
    private readonly IWriterFactory _writerFactory;

    public ChunkFileFactory(
        ITempFileFactory tempFileFactory,
        IReaderFactory readerFactory,
        IWriterFactory writerFactory)
    {
        _tempFileFactory = tempFileFactory;
        _readerFactory = readerFactory;
        _writerFactory = writerFactory;
    }

    public ChunkFile CreateChunkFile()
    {
        var file = _tempFileFactory.TempFile();
        return new(file, _readerFactory, _writerFactory);
    }
}