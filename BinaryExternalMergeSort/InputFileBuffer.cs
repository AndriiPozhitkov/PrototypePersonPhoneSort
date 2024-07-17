using BinaryExternalMergeSort.InputFileBuffers;

namespace BinaryExternalMergeSort;

public sealed class InputFileBuffer
{
    private readonly Context _context;
    private readonly FillBufferStrategy _fillBuffer;
    private readonly NextRecordStrategy _nextRecord;

    public InputFileBuffer(int size)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(
            size, 1, nameof(size));

        _context = new(size);
        _fillBuffer = new(_context);
        _nextRecord = new(_context);
    }

    public bool ScanNextRecord() =>
        _nextRecord.Scan();

    public Task<bool> Read(IReader reader) =>
        _fillBuffer.Read(reader);
}