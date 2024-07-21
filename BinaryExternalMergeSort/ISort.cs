namespace BinaryExternalMergeSort;

public interface ISort
{
    Task CreateChunks();

    Task Execute();

    Task MergeChunks();
}