namespace BinaryExternalMergeSort;

public sealed class RecordsPoolBufferFactory : IRecordsPoolBufferFactory
{
    //private const int MaxBufferSizeByte = 1_073_741_824;
    //private const int MaxBufferSizeByte = 536_870_912;
    //private const int MaxBufferSizeByte = 104_857_600;
    //private const int MaxBufferSizeByte = 10_485_760;
    private const int MaxBufferSizeByte = 2097152;

    private int _bufferSizeByte = MaxBufferSizeByte;
    private int _chunkBufferSizeByte = MaxBufferSizeByte;
    private int _chunksCount = 1;
    private long _inputFileSizeByte = 0U;

    public IRecordsPoolBuffer ChunkFileBuffer() =>
        new RecordsPoolBuffer(_chunkBufferSizeByte);

    public void ChunksCount(int chunksCount)
    {
        _chunksCount = chunksCount > 1 ? chunksCount : 1;
        _chunkBufferSizeByte = _bufferSizeByte / _chunksCount;
    }

    public IRecordsPoolBuffer RecordsPoolBuffer(FileInfo input)
    {
        _inputFileSizeByte = input.Length;

        _bufferSizeByte = _inputFileSizeByte < MaxBufferSizeByte
            ? (int)_inputFileSizeByte
            : MaxBufferSizeByte;

        return new RecordsPoolBuffer(_bufferSizeByte);
    }
}