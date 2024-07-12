namespace ExternalMergeSort;

public interface ISort : IDisposable
{
    Task CreateChunks();

    Task Execute();

    Task MergeChunks();
}