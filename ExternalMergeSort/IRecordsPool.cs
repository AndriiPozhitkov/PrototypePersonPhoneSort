namespace ExternalMergeSort;

public interface IRecordsPool
{
    Task<IChunk> ReadChunk(IReader reader);
}