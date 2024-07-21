namespace BinaryExternalMergeSort;

public sealed class RecordsPoolBufferFactory : IRecordsPoolBufferFactory
{
    private const int MaxBytes = 536_870_912;

    public IRecordsPoolBuffer ChunkFileBuffer(int chunksCount)
    {
        return new RecordsPoolBuffer(0);
    }

    public IRecordsPoolBuffer RecordsPoolBuffer(FileInfo input)
    {
        return new RecordsPoolBuffer(MaxBytes);
    }
}