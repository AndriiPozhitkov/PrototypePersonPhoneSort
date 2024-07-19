namespace BinaryExternalMergeSort;

public sealed class EmptyReader : IReader
{
    public void Dispose()
    {
    }

    public bool EndOfFile() => true;

    public Task<int> Read(byte[] buffer, int offset, int count) =>
        Task.FromResult(0);
}