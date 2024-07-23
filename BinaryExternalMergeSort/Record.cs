namespace BinaryExternalMergeSort;

public readonly struct Record(int begin, int size)
{
    private readonly int _begin = begin;
    private readonly int _size = size;

    public Record() : this(0, 0)
    {
    }

    public Record(IRecordBuffer buffer) : this(
        buffer.RecordBegin(),
        buffer.RecordEnd() - buffer.RecordBegin() + 1)
    {
    }

    public int Compare(byte[] buffer, Record y) =>
        Compare(buffer, _begin, _size, buffer, y._begin, y._size);

    private static int Compare(byte[] xb, int xi, int xs, byte[] yb, int yi, int ys)
    {
        var ms = Math.Min(xs, ys);
        var xin = xi + ms;

        while (xi < xin)
        {
            var compare = xb[xi] - yb[yi];

            if (compare != 0)
            {
                return compare;
            }

            xi++;
            yi++;
        }

        if (xs == ys) return 0;
        return xs < ys ? -1 : 1;
    }

    public int Compare2(byte[] bufferX, byte[] bufferY, Record y) =>
        Compare(bufferX, _begin, _size, bufferY, y._begin, y._size);

    public override readonly string ToString() => _begin.ToString();

    public async Task Write(byte[] buffer, IWriter writer)
    {
        await writer.Write(buffer, _begin, _size);
        await writer.WriteEOL();
    }
}