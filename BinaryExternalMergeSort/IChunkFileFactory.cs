namespace BinaryExternalMergeSort;

public interface IChunkFileFactory
{
    void ReadChunkFiles(List<WriteChunkFile> writes, List<ReadChunkFile> reads);

    WriteChunkFile WriteChunkFile();
}