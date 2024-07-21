namespace BinaryExternalMergeSort;

public sealed class WriteChunkFile
{
    private readonly FileInfo _file;
    private readonly IWriterFactory _writerFactory;

    public WriteChunkFile(
        FileInfo file,
        IWriterFactory writerFactory)
    {
        _file = file;
        _writerFactory = writerFactory;
    }

    public ReadChunkFile ReadChunkFile(
            IRecordsPoolBuffer buffer,
            IReaderFactory readerFactory) =>
        new(buffer, _file, readerFactory.Reader(_file));

    public async Task Write(IChunk chunk)
    {
        using var writer = _writerFactory.Writer(_file);
        await chunk.Write(writer);
    }
}