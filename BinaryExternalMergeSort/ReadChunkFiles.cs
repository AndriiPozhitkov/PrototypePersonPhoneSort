namespace BinaryExternalMergeSort;

public sealed class ReadChunkFiles(IChunkFileFactory factory) : IDisposable
{
    private readonly List<ReadChunkFile> _readChunkFiles = [];

    public void Dispose()
    {
        foreach (var readChunkFiles in _readChunkFiles)
            readChunkFiles.Dispose();

        _readChunkFiles.Clear();
        _readChunkFiles.TrimExcess();
    }

    public async Task MergeChunks(
        List<WriteChunkFile> writes,
        IWriter output)
    {
        factory.ReadChunkFiles(writes, _readChunkFiles);
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