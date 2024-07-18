namespace BinaryExternalMergeSort;

public interface IRecordBuffer
{
    int RecordBegin();

    Task Write(int recordBegin, IWriter writer);
}