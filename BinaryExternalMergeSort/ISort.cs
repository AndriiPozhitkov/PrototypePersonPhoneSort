namespace BinaryExternalMergeSort;

public interface ISort : IDisposable
{
    Task CreateChunks();

    Task Execute();

    Task MergeChunks();
}