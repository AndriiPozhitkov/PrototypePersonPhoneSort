namespace BinaryExternalMergeSort;

public interface IWriter : IDisposable
{
    Task Write(byte[] buffer, int offset, int count);

    Task WriteEOL();
}