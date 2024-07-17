using BinaryExternalMergeSort.InputFileBuffers;

namespace BinaryExternalMergeSort;

public sealed class InputFileBuffer
{
    private readonly Context _context;
    private readonly FillBufferStrategy _fillBuffer;
    private readonly NextLineStrategy _nextLine;

    public InputFileBuffer(int size)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(
            size, 1, nameof(size));

        _context = new(size);
        _fillBuffer = new(_context);
        _nextLine = new(_context);
    }

    public int NextLineIndex() =>
        _nextLine.Index();

    public Task<bool> Read(IReader reader) =>
        _fillBuffer.Read(reader);
}