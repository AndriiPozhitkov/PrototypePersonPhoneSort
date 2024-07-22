using BinaryExternalMergeSort.RecordsPoolBuffers;

namespace BinaryExternalMergeSort;

public readonly struct Record(int begin)
{
    private const int EOL = -1;

    private readonly int _begin = begin;

    public Record() : this(0)
    {
    }

    public Record(IRecordBuffer buffer) : this(buffer.RecordBegin())
    {
    }

    public int Compare(byte[] buffer, Record y) =>
        Compare(buffer, _begin, buffer, y._begin);

    private static int Compare(byte[] bufferX, int xi, byte[] bufferY, int yi)
    {
        var sx = EOL;
        var sy = EOL;

        while (xi <= bufferX.Length && yi <= bufferY.Length)
        {
            sx = S(bufferX, xi);
            sy = S(bufferY, yi);

            if (sx == EOL && sy == EOL) return 0;
            if (sx == EOL) return -1;
            if (sy == EOL) return 1;

            int compare = sx - sy;
            if (compare != 0) return compare;

            xi++;
            yi++;
        }

        return 0;
    }

    private static int S(byte[] buffer, int i)
    {
        if (i < buffer.Length)
        {
            var s = buffer[i];
            return s.IsEOL() ? EOL : s;
        }
        else return EOL;
    }

    public int Compare2(byte[] bufferX, byte[] bufferY, Record y) =>
        Compare(bufferX, _begin, bufferY, y._begin);

    public override readonly string ToString() => _begin.ToString();

    public async Task Write(byte[] buffer, IWriter writer)
    {
        await writer.Write(buffer, _begin, RecordSize(buffer));
        await writer.WriteEOL();
    }

    private int RecordSize(byte[] buffer)
    {
        var end = _begin;

        for (; end < buffer.Length; end++)
            if (buffer[end].IsEOL()) break;

        return end - _begin; // last symbol is EOL, so no (+ 1)
    }
}