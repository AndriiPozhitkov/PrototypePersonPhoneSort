namespace BinaryExternalMergeSort;

public sealed class Sort : ISort
{
    private readonly IChunksPool _chunksPool;
    private readonly FileInfo _inputFile;
    private readonly FileInfo _outputFile;
    private readonly IRecordsPoolFactory _recordsPoolFactory;

    public Sort(
        IChunksPool chunksPool,
        FileInfo inputFile,
        FileInfo outputFile,
        IRecordsPoolFactory recordsPoolFactory)
    {
        _chunksPool = chunksPool;
        _inputFile = inputFile;
        _outputFile = outputFile;
        _recordsPoolFactory = recordsPoolFactory;
    }

    public async Task Execute()
    {
        await CreateChunks();
        await MergeChunks();
    }

    public async Task CreateChunks()
    {
        using var recordsPool = _recordsPoolFactory.RecordsPool(_inputFile);
        using var input = new FileReader(_inputFile);
        var chunk = await recordsPool.ReadChunk(input);
        while (chunk.NotEmpty())
        {
            chunk.Sort();
            await _chunksPool.CreateChunkFile(chunk);
            chunk = await recordsPool.ReadChunk(input);
        }
    }

    public async Task MergeChunks()
    {
        using var output = new FileWriter(_outputFile);
        await _chunksPool.MergeChunks(output);
    }
}