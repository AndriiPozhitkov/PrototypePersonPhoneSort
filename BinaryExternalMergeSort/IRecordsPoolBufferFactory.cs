namespace BinaryExternalMergeSort;

public interface IRecordsPoolBufferFactory
{
    IRecordsPoolBuffer ChunkFileBuffer();

    void ChunksCount(int chunksCount);

    IRecordsPoolBuffer RecordsPoolBuffer(FileInfo input);
}