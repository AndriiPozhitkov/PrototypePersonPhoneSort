namespace BinaryExternalMergeSort;

public sealed class ChunksPool : IChunksPool
{
    private readonly IChunkFileFactory _chunkFileFactory;
    private readonly List<ReadChunkFile> _readChunkFiles = [];
    private readonly List<WriteChunkFile> _writeChunkFiles = [];

    public ChunksPool(IChunkFileFactory chunkFileFactory) =>
        _chunkFileFactory = chunkFileFactory;

    public async Task CreateChunkFile(IChunk chunk)
    {
        var writeChunkFile = _chunkFileFactory.WriteChunkFile();
        _writeChunkFiles.Add(writeChunkFile);
        await writeChunkFile.Write(chunk);
    }

    public void Dispose()
    {
        foreach (var readChunkFiles in _readChunkFiles)
            readChunkFiles.Dispose();

        _readChunkFiles.Clear();
        _readChunkFiles.TrimExcess();
    }

    public async Task MergeChunks(IWriter output)
    {
        _chunkFileFactory.ReadChunkFiles(_writeChunkFiles, _readChunkFiles);
        await MergeChunks0(output);
    }

    private async Task MergeChunks0(IWriter output)
    {
        var minimal = await Minimal();
        while (minimal.IsReaded())
        {
            await minimal.WriteMinimalRecordToOutput(output);
            minimal = await Minimal();
        }
    }

    private async Task<ReadChunkFile> Minimal()
    {
        var minimal = _readChunkFiles[0];
        await minimal.ReadNextRecord();

        for (var i = 1; i < _readChunkFiles.Count; i++)
        {
            var chunkFile = _readChunkFiles[i];
            await chunkFile.ReadNextRecord();
            minimal = minimal.MinimalRecord(_readChunkFiles[i]);
        }
        return minimal;
    }
}