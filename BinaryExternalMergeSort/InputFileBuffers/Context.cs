namespace BinaryExternalMergeSort.InputFileBuffers;

public sealed class Context(int capacity)
{
    public readonly byte[] Buffer = new byte[capacity];

    public int LastLineBegin;
    public int ReadNumber;
    public int Size;
}