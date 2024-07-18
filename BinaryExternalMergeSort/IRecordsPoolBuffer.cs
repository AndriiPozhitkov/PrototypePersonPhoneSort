namespace BinaryExternalMergeSort;

public interface IRecordsPoolBuffer : IComparer<Record>, IRecordBuffer
{
    Task<bool> Read(IReader reader);

    bool ScanNextRecord();

    Task Write(Record record, IWriter writer);
}