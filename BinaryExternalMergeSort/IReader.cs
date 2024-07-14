namespace BinaryExternalMergeSort;

public interface IReader : IDisposable
{
    Task<int> Read(byte[] buffer, int offset, int count);
}