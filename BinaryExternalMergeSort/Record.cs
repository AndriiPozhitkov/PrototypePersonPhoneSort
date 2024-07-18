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

    public readonly int Compare(byte[] buffer, Record y)
    {
        const int EOL = -1;
        var N = buffer.Length;

        int S(int i)
        {
            if (i < N)
            {
                var s = buffer[i];
                return s.IsEOL() ? EOL : s;
            }
            else return EOL;
        }

        var xi = _begin;
        var yi = y._begin;

        var sx = EOL;
        var sy = EOL;

        while (xi <= N && yi <= N)
        {
            sx = S(xi);
            sy = S(yi);

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

    public void SetBegin(IRecordBuffer buffer) =>
        _begin = buffer.RecordBegin();

    public readonly async Task Write(byte[] buffer, IWriter writer)
    {
        await writer.Write(buffer, _begin, RecordSize(buffer));
        await writer.WriteEOL();
    }

    private readonly int RecordSize(byte[] buffer)
    {
        var end = _begin;

        for (; end < buffer.Length; end++)
            if (buffer[end].IsEOL()) break;

        return end - _begin + 1;
    }
}