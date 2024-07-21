namespace BinaryExternalMergeSort;

public interface IRecordsPool :
    IChunk,
    IDisposable
{
    Task<IChunk> ReadChunk(IReader reader);
}