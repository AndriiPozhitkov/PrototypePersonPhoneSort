namespace BinaryExternalMergeSort;

public sealed class InputFileBufferContext(int size)
{
    public readonly byte[] _buffer = new byte[size];
    public int _readed;

    public async Task Read(IReader reader)
    {
        var offset = 0;
        var count = _buffer.Length;
        _readed = await reader.Read(_buffer, offset, count);
    }

    public byte TestByte(int index) => _buffer[index];
}