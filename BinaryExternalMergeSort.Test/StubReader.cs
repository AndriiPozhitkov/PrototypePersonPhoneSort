namespace BinaryExternalMergeSort.Test;

internal sealed class StubReader : IReader
{
    private const char FD = ';';
    private const char CR = '\r';
    private const char LF = '\n';

    private readonly int _lineSize;

    private byte[] _buffer;
    private int _index;

    public StubReader(byte[] buffer, int lineSize)
    {
        _lineSize = lineSize;
        _buffer = buffer;
    }

    public void Dispose() => _buffer = [];

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

    internal static StubReader Lines(int count) =>
        Lines(count, "L", "F", "M", "P");

    internal static StubReader Lines(
        int count, string last, string first, string middle, string phone)
    {
        using var memory = new MemoryStream();
        using var w = new StreamWriter(memory);
        var lineSize = 0;
        for (int i = 0; i < count; i++)
        {
            w.Write(last);
            w.Write(FD);
            w.Write(first);
            w.Write(FD);
            w.Write(middle);
            w.Write(FD);
            w.Write(phone);
            w.Write(CR);
            w.Write(LF);
        }
        w.Flush();
        lineSize = (int)(memory.Length / count);
        return new(memory.ToArray(), lineSize);
    }

    internal int LineSize_0_5() => _lineSize / 2;

    internal int LineSize_1_0() => _lineSize;

    internal int LineSize_1_5() => _lineSize * 3 / 2;
}