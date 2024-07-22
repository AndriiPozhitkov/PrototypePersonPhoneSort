namespace BinaryExternalMergeSort;

public sealed class SortWorkTime(
        ITrace trace,
        ISort decoratee) :
    ISort
{
    public async Task CreateChunks()
    {
        using var scope = trace.Scope(
            nameof(ISort), nameof(CreateChunks));

        await decoratee.CreateChunks();
    }

    public async Task Execute()
    {
        using var scope = trace.Scope(
            nameof(ISort), nameof(Execute));

        await decoratee.Execute();
    }

    public async Task MergeChunks()
    {
        using var scope = trace.Scope(
            nameof(ISort), nameof(MergeChunks));

        await decoratee.MergeChunks();
    }
}