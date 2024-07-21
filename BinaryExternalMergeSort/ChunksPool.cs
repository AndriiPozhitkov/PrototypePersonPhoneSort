namespace BinaryExternalMergeSort;

public sealed class ChunksPool(IChunkFileFactory factory) : IChunksPool
{
    private readonly List<WriteChunkFile> _writes = [];

    public async Task CreateChunkFile(IChunk chunk)
    {
        var file = factory.WriteChunkFile();
        _writes.Add(file);
        await file.Write(chunk);
    }

    public async Task MergeChunks(IWriter output)
    {
        using var reads = new ReadChunkFiles(factory);
        await reads.MergeChunks(_writes, output);
    }
}