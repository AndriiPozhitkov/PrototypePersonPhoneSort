namespace ExternalMergeSort;

public interface IReaderFactory
{
    IReader Reader(FileInfo file);
}