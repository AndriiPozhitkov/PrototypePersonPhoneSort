namespace BinaryExternalMergeSort;

public sealed class ChunksPoolWorkTime(
        ITrace trace,
        IChunksPool decoratee) :
    IChunksPool
{
    public async Task CreateChunkFile(IChunk chunk)
    {
        using var scope = trace.Scope(
            nameof(IChunksPool), nameof(CreateChunkFile));

        await decoratee.CreateChunkFile(chunk);
    }

    public async Task MergeChunks(IWriter output)
    {
        using var scope = trace.Scope(
            nameof(IChunksPool), nameof(MergeChunks));

        await decoratee.MergeChunks(output);
    }
}