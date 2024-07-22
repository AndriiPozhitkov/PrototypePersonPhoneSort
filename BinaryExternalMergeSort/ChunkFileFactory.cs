namespace BinaryExternalMergeSort;

public sealed class ChunkFileFactory(
        IRecordsPoolBufferFactory bufferFactory,
        IReaderFactory readerFactory,
        ITempFileFactory tempFileFactory,
        IWriterFactory writerFactory) :
    IChunkFileFactory
{
    private int _chunksNumber;

    public void ReadChunkFiles(
        List<WriteChunkFile> writes,
        List<ReadChunkFile> reads)
    {
        bufferFactory.ChunksCount(writes.Count);

        foreach (var write in writes)
        {
            var buffer = bufferFactory.ChunkFileBuffer();
            var read = write.ReadChunkFile(buffer, readerFactory);
            reads.Add(read);
        }

        writes.Clear();
        writes.TrimExcess();
    }

    public WriteChunkFile WriteChunkFile()
    {
        _chunksNumber++;
        var file = tempFileFactory.TempFile();
        return new(file, _chunksNumber, writerFactory);
    }
}