namespace ExternalMergeSort;

public sealed class Sort : ISort
{
    private readonly IChunksPool _chunksPool;
    private readonly FileInfo _inputFile;
    private readonly FileInfo _outputFile;
    private readonly IRecordsPool _recordsPool;

    public Sort(
        IChunksPool chunksPool,
        FileInfo inputFile,
        FileInfo outputFile,
        IRecordsPool recordsPool)
    {
        _chunksPool = chunksPool;
        _inputFile = inputFile;
        _outputFile = outputFile;
        _recordsPool = recordsPool;
    }

    public void Dispose() =>
        _chunksPool.Dispose();

    public async Task Execute()
    {
        await CreateChunks();
        await MergeChunks();
    }

    public async Task CreateChunks()
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

    public async Task MergeChunks()
    {
        using var output = new FileWriter(_outputFile);
        await _chunksPool.MergeChunks(output);
    }
}