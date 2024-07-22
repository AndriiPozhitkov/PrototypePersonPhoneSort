namespace BinaryExternalMergeSort;

public sealed class WriteChunkFile(
    FileInfo file,
    int number,
    IWriterFactory writerFactory)
{
    public ReadChunkFile ReadChunkFile(
            IRecordsPoolBuffer buffer,
            IReaderFactory readerFactory) =>
        new(buffer, file, number, readerFactory.Reader(file));

    public async Task Write(IChunk chunk)
    {
        using var writer = writerFactory.Writer(file);
        await chunk.Write(writer);
    }
}