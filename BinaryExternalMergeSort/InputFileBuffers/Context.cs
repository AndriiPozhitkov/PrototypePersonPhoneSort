namespace BinaryExternalMergeSort.InputFileBuffers;

public sealed class Context(int capacity)
{
    public readonly byte[] Buffer = new byte[capacity];

    public int Offset;
    public int LastLineBegin;
    public int ReadCount;
    public int Readed;
    public int Size;

    public byte TestByte(int index) => Buffer[index];

    public char TestChar(int index) => (char)Buffer[index];
}