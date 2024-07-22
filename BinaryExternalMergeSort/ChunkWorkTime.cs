namespace BinaryExternalMergeSort;

public sealed class ChunkWorkTime(
        ITrace trace,
        IChunk decoratee) :
    IChunk
{
    public bool NotEmpty() => decoratee.NotEmpty();

    public void Sort()
    {
        using var scope = trace.Scope(
            nameof(IChunk), nameof(Sort));

        decoratee.Sort();
    }

    public async Task Write(IWriter writer)
    {
        using var scope = trace.Scope(
            nameof(IChunk), nameof(Write));

        await decoratee.Write(writer);
    }
}