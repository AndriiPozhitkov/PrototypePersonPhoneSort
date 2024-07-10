namespace ExternalMergeSort;

public sealed class Sort : IDisposable
{
    private readonly ChunksPool _chunksPool;
    private readonly FileInfo _inputFile;
    private readonly FileInfo _outputFile;
    private readonly RecordsPool _recordsPool;

    public Sort(
        IChunkFileFactory chunkFileFactory,
        IRecordsPoolSize recordsPoolSize,
        FileInfo inputFile,
        FileInfo outputFile)
    {
        _recordsPool = new(recordsPoolSize);
        _chunksPool = new(chunkFileFactory);
        _inputFile = inputFile;
        _outputFile = outputFile;
    }

    public void Dispose() =>
        _chunksPool.Dispose();

    public async Task Execute()
    {
        await CreateChunks();
        await MergeChunks();
    }

    private async Task CreateChunks()
    {
        using var input = new FileReader(_inputFile);

        var chunk = await _recordsPool.ReadChunk(input);
        while (chunk.NotEmpty())
        {
            chunk.Sort();
            await _chunksPool.CreateChunkFile(chunk);

            chunk = await _recordsPool.ReadChunk(input);
        }
    }

    private async Task MergeChunks()
    {
        using var output = new FileWriter(_outputFile);
        await _chunksPool.MergeChunks(output);
    }
}