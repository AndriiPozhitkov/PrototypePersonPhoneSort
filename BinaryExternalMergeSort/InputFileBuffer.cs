using BinaryExternalMergeSort.InputFileBuffers;

namespace BinaryExternalMergeSort;

public sealed class InputFileBuffer
{
    private readonly Context _context;
    private readonly BufferStrategy _buffer;
    private readonly NextRecordStrategy _nextRecord;

    public InputFileBuffer(int size)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(
            size, 1, nameof(size));

        _context = new(size);
        _buffer = new(_context);
        _nextRecord = new(_context);
    }

    public Task<bool> Read(IReader reader) => _buffer.Read(reader);

    public bool ScanNextRecord() => _nextRecord.Scan();

    public int TestRecordBegin() => _context.RecordBegin;

    public int TestRecordEnd() => _context.RecordEnd;
}