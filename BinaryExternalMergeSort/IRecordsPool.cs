namespace BinaryExternalMergeSort;

public interface IRecordsPool : IChunk
{
    Task<IChunk> ReadChunk(IReader reader);
}