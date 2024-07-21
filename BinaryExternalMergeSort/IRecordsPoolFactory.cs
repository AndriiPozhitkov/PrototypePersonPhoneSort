namespace BinaryExternalMergeSort;

public interface IRecordsPoolFactory
{
    IRecordsPool RecordsPool(FileInfo input);
}