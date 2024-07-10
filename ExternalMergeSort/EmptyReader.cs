namespace ExternalMergeSort;

public sealed class EmptyReader : IReader
{
    public void Dispose()
    {
    }

    public Task<string?> ReadLine() =>
        Task.FromResult<string?>(null);
}