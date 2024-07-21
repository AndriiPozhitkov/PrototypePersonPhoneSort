namespace BinaryExternalMergeSort;

public interface IRecordsPoolBufferFactory
{
    IRecordsPoolBuffer ChunkFileBuffer(int chunksCount);

    IRecordsPoolBuffer RecordsPoolBuffer(FileInfo input);
}