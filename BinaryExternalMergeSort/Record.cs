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
        const int CR = 0x0D; // 13 \r
        const int LF = 0x0A; // 10 \n

        var xin = bufferX.Length;
        var yin = bufferY.Length;

        while (xi < xin && yi < yin)
        {
            int sx = bufferX[xi];
            if (sx == CR || sx == LF)
            {
                sx = EOL;
            }

            int sy = bufferY[yi];
            if (sy == CR || sy == LF)
            {
                sy = EOL;
            }

            if (sx == EOL && sy == EOL) return 0;
            if (sx == EOL) return -1;
            if (sy == EOL) return 1;

            var compare = sx - sy;
            if (compare != 0) return compare;

            xi++;
            yi++;
        }

        while (xi <= xin && yi <= yin)
        {
            int sx = EOL;
            if (xi < xin)
            {
                sx = bufferX[xi];
                if (sx == CR || sx == LF)
                {
                    sx = EOL;
                }
            }

            int sy = EOL;
            if (yi < yin)
            {
                sy = bufferY[yi];
                if (sy == CR || sy == LF)
                {
                    sy = EOL;
                }
            }

            if (sx == EOL && sy == EOL) return 0;
            if (sx == EOL) return -1;
            if (sy == EOL) return 1;

            var compare = sx - sy;
            if (compare != 0) return compare;

            xi++;
            yi++;
        }

        return 0;
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