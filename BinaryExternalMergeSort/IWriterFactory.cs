namespace BinaryExternalMergeSort;

public interface IWriterFactory
{
    IWriter Writer(FileInfo file);
}