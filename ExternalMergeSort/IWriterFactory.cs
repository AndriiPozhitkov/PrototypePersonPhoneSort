
namespace ExternalMergeSort;

public interface IWriterFactory
{
    IWriter Writer(FileInfo file);
}