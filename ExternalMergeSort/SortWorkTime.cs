namespace ExternalMergeSort;

public sealed class SortWorkTime(
        ITrace trace,
        ISort decoratee) :
    ISort
{
    public async Task CreateChunks()
    {
        using var scope = trace.Scope(nameof(CreateChunks));
        await decoratee.CreateChunks();
    }

    public void Dispose() =>
        decoratee.Dispose();

    public async Task Execute()
    {
        using var scope = trace.Scope(nameof(Execute));
        await decoratee.Execute();
    }

    public async Task MergeChunks()
    {
        using var scope = trace.Scope(nameof(MergeChunks));
        await decoratee.MergeChunks();
    }
}