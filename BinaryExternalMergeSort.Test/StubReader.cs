namespace BinaryExternalMergeSort.Test;

internal sealed class StubReader : IReader
{
    private const char CR = '\r';
    private const char FD = ';';
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

    public bool EndOfFile() => _index >= _buffer.Length;

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

    internal static StubReader Lines(int count)
    {
        int n = -1;
        char N()
        {
            n++;
            if (n > 9) n = 0;
            return (char)(0x30 + n);
        }

        using var memory = new MemoryStream();
        using var w = new StreamWriter(memory);
        for (int i = 0; i < count; i++)
        {
            n = i - 1;
            w.Write(N());
            w.Write(FD);
            w.Write(N());
            w.Write(FD);
            w.Write(N());
            w.Write(FD);
            w.Write(N());
            w.Write(CR);
            w.Write(LF);
        }
        w.Flush();
        var lineSize = (int)(memory.Length / count);
        return new(memory.ToArray(), lineSize);
    }

    internal static StubReader Lines(params string[] lines)
    {
        using var memory = new MemoryStream();
        using var w = new StreamWriter(memory);
        foreach (var line in lines)
        {
            w.Write(line);
            w.Write(CR);
            w.Write(LF);
        }
        w.Flush();
        var lineSize = (int)(memory.Length / lines.Length);
        return new(memory.ToArray(), lineSize);
    }

    internal static StubReader LinesLastWoEOL(params string[] lines)
    {
        using var memory = new MemoryStream();
        using var w = new StreamWriter(memory);
        var i = 0;
        for (; i < lines.Length - 1; i++)
        {
            w.Write(lines[i]);
            w.Write(CR);
            w.Write(LF);
        }
        if (i < lines.Length)
        {
            w.Write(lines[i]);
        }
        w.Flush();
        var lineSize = (int)(memory.Length / lines.Length);
        return new(memory.ToArray(), lineSize);
    }

    internal int HalfLine() => _lineSize / 2;

    internal int OneAndHalfLine() => _lineSize * 3 / 2;

    internal int OneLine() => _lineSize;

    internal int SameSize() => _buffer.Length;

    internal int TenLines() => 10 * _lineSize;
}