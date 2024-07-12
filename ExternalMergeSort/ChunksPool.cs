namespace ExternalMergeSort;

public sealed class ChunksPool : IChunksPool
{
    private readonly IChunkFileFactory _chunkFileFactory;
    private readonly List<ChunkFile> _chunkFiles = [];

    public ChunksPool(IChunkFileFactory chunkFileFactory) =>
        _chunkFileFactory = chunkFileFactory;

    public async Task CreateChunkFile(IChunk chunk)
    {
        var chunkFile = _chunkFileFactory.CreateChunkFile();
        _chunkFiles.Add(chunkFile);
        await chunkFile.Write(chunk);
    }

    public void Dispose()
    {
        foreach (var chunkFile in _chunkFiles)
            chunkFile.Dispose();
    }

    public async Task MergeChunks(IWriter output)
    {
        var minimal = await Minimal();

        while (minimal.IsReaded())
        {
            await minimal.WriteMinimalRecordToOutput(output);

            minimal = await Minimal();
        }
    }

    private async Task<ChunkFile> Minimal()
    {
        var minimal = _chunkFiles[0];
        await minimal.ReadNextRecord();

        for (var i = 1; i < _chunkFiles.Count; i++)
        {
            var chunkFile = _chunkFiles[i];
            await chunkFile.ReadNextRecord();
            minimal = minimal.MinimalRecord(_chunkFiles[i]);
        }
        return minimal;
    }
}