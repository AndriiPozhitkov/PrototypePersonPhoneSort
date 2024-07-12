namespace ExternalMergeSort;

public sealed class ChunksPoolWorkTime(
        ITrace trace,
        IChunksPool decoratee) :
    IChunksPool
{
    public async Task CreateChunkFile(IChunk chunk)
    {
        using var scope = trace.Scope(nameof(CreateChunkFile));
        await decoratee.CreateChunkFile(chunk);
    }

    public void Dispose() =>
        decoratee.Dispose();

    public async Task MergeChunks(IWriter output)
    {
        using var scope = trace.Scope(nameof(MergeChunks));
        await decoratee.MergeChunks(output);
    }
}