namespace BinaryExternalMergeSort;

public sealed class RecordsPoolFactoryWorkTime(
        ITrace trace,
        IRecordsPoolFactory decoratee) :
    IRecordsPoolFactory
{
    public IRecordsPool RecordsPool(FileInfo input)
    {
        using var scope = trace.Scope(
            nameof(IRecordsPoolFactory), nameof(RecordsPool));

        return new RecordsPoolWorkTime(trace, decoratee.RecordsPool(input));
    }
}