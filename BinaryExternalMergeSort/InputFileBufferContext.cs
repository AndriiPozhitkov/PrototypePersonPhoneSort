namespace BinaryExternalMergeSort;

public sealed class InputFileBufferContext
{
    private readonly byte[] _buffer;

    private int _readed;
    private int _offset;
    private int _count;

    public InputFileBufferContext(int size)
    {
        _buffer = new byte[size];
        _offset = 0;
        _count = _buffer.Length;
    }

    public bool IsReadedGtZero => _readed > 0;
    public int Readed => _readed;
    public byte this[int index] => _buffer[index];

    public void CopyPartialLineToStart(int index)
    {
        _offset = _buffer.Length - index;
        Array.Copy(_buffer, index, _buffer, 0, _offset);
    }

    public async Task Read(IReader reader)
    {
        _offset = 0;
        _count = _buffer.Length;
        _readed = await reader.Read(_buffer, _offset, _count);
    }
}