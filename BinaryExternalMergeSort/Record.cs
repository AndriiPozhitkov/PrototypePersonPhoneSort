using BinaryExternalMergeSort.InputFileBuffers;

namespace BinaryExternalMergeSort;

public struct Record(int begin)
{
    private int _begin = begin;

    public Record() : this(0)
    {
    }

    public Record(IRecordBuffer buffer) : this(buffer.RecordBegin())
    {
    }

    public readonly int Begin => _begin;

    public int Compare(byte[] buffer, Record y)
    {
        var N = buffer.Length;

        for (int xi = _begin, yi = y._begin; xi < N && yi < N; xi++, yi++)
        {
            var sx = buffer[xi];
            if (sx.IsEOL()) return 0;

            var sy = buffer[yi];
            if (sy.IsEOL()) return 0;

            int compare = sx - sy;
            if (compare != 0) return compare;
        }

        return 0;
    }

    public void SetBegin(IRecordBuffer buffer) => _begin = buffer.RecordBegin();

    public readonly async Task Write(IRecordBuffer buffer, IWriter writer)
    {
        await buffer.Write(_begin, writer);
        await writer.WriteEOL();
    }
}