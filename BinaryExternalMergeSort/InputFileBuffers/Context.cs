namespace BinaryExternalMergeSort.InputFileBuffers;

public sealed class Context
{
    private readonly byte[] _buffer;

    private int _count;
    private int _offset;
    private int _readed;

    public Context(int size)
    {
        _buffer = new byte[size];
        _offset = 0;
        _count = _buffer.Length;
    }

    public bool IsReadedGtZero => _readed > 0;
    public int Readed => _readed;
    public byte this[int index] => _buffer[index];

    public void CopyPartialLineToBufferStart(int lineBeginIndex)
    {
        _offset = _buffer.Length - lineBeginIndex;
        Array.Copy(_buffer, lineBeginIndex, _buffer, 0, _offset);
    }

    public async Task Read(IReader reader)
    {
        _offset = 0;
        _count = _buffer.Length;
        _readed = await reader.Read(_buffer, _offset, _count);
    }
}