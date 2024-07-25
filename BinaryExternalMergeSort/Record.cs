using System.Runtime.CompilerServices;

namespace BinaryExternalMergeSort;

public readonly struct Record : IEquatable<Record>
{
    private readonly int _begin;
    private readonly int _size;

    public Record() : this(0, 0)
    {
    }

    public Record(IRecordBuffer buffer) : this(
        buffer.RecordBegin(),
        buffer.RecordSize())
    {
    }

    public Record(int begin, int size)
    {
        _begin = begin;
        _size = size;
    }

    public static bool operator !=(Record left, Record right) =>
        !left.Equals(right);

    public static bool operator ==(Record left, Record right) =>
        left.Equals(right);

    public int Compare(byte[] buffer, Record y) =>
        Compare(_begin, y._begin, _size, y._size, buffer, buffer);

    public int Compare2(byte[] bufferX, byte[] bufferY, Record y) =>
        Compare(_begin, y._begin, _size, y._size, bufferX, bufferY);

    public override bool Equals(object? right) =>
        right is Record record && Equals(record);

    public bool Equals(Record right) =>
        _begin == right._begin && _size == right._size;

    public override int GetHashCode() =>
        HashCode.Combine(_begin, _size);

    public override readonly string ToString() =>
        string.Concat("(", _begin.ToString(), ", ", _size.ToString(), ")");

    public async Task Write(byte[] buffer, IWriter writer)
    {
        await writer.Write(buffer, _begin, _size);
        await writer.WriteEOL();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int Compare(int xi, int yi, int xs, int ys, byte[] xb, byte[] yb)
    {
        var xin = xi + Math.Min(xs, ys);

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

        return xs - ys;
    }
}