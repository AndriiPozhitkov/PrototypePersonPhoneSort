namespace BinaryExternalMergeSort;

public interface IChunkFileRecord
{
    int CompareTo(IChunkFileRecord record);

    bool IsReaded();

    Task TryRead(IReader reader);

    Task Write(IWriter output);
}