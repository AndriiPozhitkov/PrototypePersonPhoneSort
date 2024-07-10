namespace ExternalMergeSort;

public sealed class RecordsPoolSizeFixed : IRecordsPoolSize
{
    private readonly int _size;

    public RecordsPoolSizeFixed(int size) =>
        _size = size;

    public List<Record> Records()
    {
        var records = new List<Record>(_size);

        for (var i = 0; i < _size; i++)
            records.Add(new Record());

        return records;
    }
}