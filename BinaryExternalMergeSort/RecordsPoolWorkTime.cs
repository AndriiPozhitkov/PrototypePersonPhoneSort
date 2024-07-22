namespace BinaryExternalMergeSort;

public sealed class RecordsPoolWorkTime(
        ITrace trace,
        IRecordsPool decoratee) :
    IRecordsPool
{
    public void Dispose() => decoratee.Dispose();

    public bool NotEmpty() => decoratee.NotEmpty();

    public async Task<IChunk> ReadChunk(IReader reader)
    {
        using var scope = trace.Scope(
            nameof(IRecordsPool), nameof(ReadChunk));

        return new ChunkWorkTime(trace, await decoratee.ReadChunk(reader));
    }

    public void Sort() => decoratee.Sort();

    public Task Write(IWriter writer) => decoratee.Write(writer);
}