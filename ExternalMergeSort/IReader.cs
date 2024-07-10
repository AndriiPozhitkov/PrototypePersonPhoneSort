namespace ExternalMergeSort;

public interface IReader : IDisposable
{
    Task<string?> ReadLine();
}