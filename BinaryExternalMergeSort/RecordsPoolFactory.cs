namespace BinaryExternalMergeSort;

public sealed class RecordsPoolFactory(
        IRecordsPoolBufferFactory bufferFactory) :
    IRecordsPoolFactory
{
    public IRecordsPool RecordsPool(FileInfo input) =>
        new RecordsPool(bufferFactory.RecordsPoolBuffer(input));
}