namespace BinaryExternalMergeSort;

public interface IReader : IDisposable
{
    bool EndOfFile();

    Task<int> Read(byte[] buffer, int offset, int count);
}