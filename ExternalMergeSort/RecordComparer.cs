namespace ExternalMergeSort;

public sealed class RecordComparer : IComparer<Record>
{
    public int Compare(Record? x, Record? y)
    {
        if (x == null)
        {
            return y == null ? 0 : -1;
        }
        else
        {
            return x.CompareTo(y);
        }
    }
}