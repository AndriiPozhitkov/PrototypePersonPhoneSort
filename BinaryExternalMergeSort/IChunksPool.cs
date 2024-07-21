namespace BinaryExternalMergeSort;

public interface IChunksPool
{
    Task CreateChunkFile(IChunk chunk);

    Task MergeChunks(IWriter output);
}