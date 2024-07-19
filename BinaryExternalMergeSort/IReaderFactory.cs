namespace BinaryExternalMergeSort;

public interface IReaderFactory
{
    IReader Reader(FileInfo file);
}