namespace BinaryExternalMergeSort;

public interface IRecordsPoolBuffer : IComparer<Record>, IRecordBuffer
{
    int Compare0(Record x, IRecordsPoolBuffer bufferY, Record y);

    int Compare1(byte[] bufferX, Record x, Record y);

    Task<bool> Read(IReader reader);

    bool ScanNextRecord();

    Task Write(Record record, IWriter writer);
}