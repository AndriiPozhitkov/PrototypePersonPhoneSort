using BinaryExternalMergeSort.RecordsPoolBuffers;

namespace BinaryExternalMergeSort;

public sealed class RecordsPoolBuffer : IRecordsPoolBuffer
{
    private readonly BufferStrategy _buffer;
    private readonly Context _context;
    private readonly NextRecordStrategy _nextRecord;

    public RecordsPoolBuffer(int size)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(
            size, 1, nameof(size));

        _context = new(size);
        _buffer = new(_context);
        _nextRecord = new(_context);
    }

    public int Compare(Record x, Record y) => x.Compare(_context.Buffer, y);

    public int Compare0(Record x, IRecordsPoolBuffer bufferY, Record y) =>
        bufferY.Compare1(_context.Buffer, x, y);

    public int Compare1(byte[] bufferX, Record x, Record y) =>
        x.Compare2(bufferX, _context.Buffer, y);

    public void Dispose()
    {
        _context.Buffer = [];
    }

    public Task<bool> Read(IReader reader) => _buffer.Read(reader);

    public int RecordBegin() => _context.RecordBegin;

    public int RecordEnd() => _context.RecordEnd;

    public bool ScanNextRecord() => _nextRecord.Scan();

    public Task Write(Record record, IWriter writer) =>
        record.Write(_context.Buffer, writer);
}