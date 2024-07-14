namespace BinaryExternalMergeSort.Test;

internal abstract class StubReaderBase : IReader
{
    private readonly byte[] _buffer;

    private int _index;

    protected StubReaderBase(byte[] buffer) =>
        _buffer = buffer;

    public void Dispose()
    {
    }

    public Task<int> Read(byte[] buffer, int offset, int count)
    {
        var readed = 0;

        if (_index < _buffer.Length)
        {
            var rest = _buffer.Length - _index;
            readed = Math.Min(rest, count);
            Array.Copy(_buffer, _index, buffer, offset, readed);
            _index += readed;
        }

        return Task.FromResult(readed);
    }
}