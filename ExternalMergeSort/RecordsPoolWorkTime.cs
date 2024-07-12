namespace ExternalMergeSort;

public sealed class RecordsPoolWorkTime(
        ITrace trace,
        IRecordsPool decoratee) :
    IRecordsPool
{
    public async Task<IChunk> ReadChunk(IReader reader)
    {
        using var scope = trace.Scope(
            nameof(IRecordsPool), nameof(ReadChunk));

        return new ChunkWorkTime(trace, await decoratee.ReadChunk(reader));
    }
}