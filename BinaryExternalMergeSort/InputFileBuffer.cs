using BinaryExternalMergeSort.InputFileBuffers;

namespace BinaryExternalMergeSort;

public sealed class InputFileBuffer
{
    public const int MinBuffer = 4096;

    private readonly Context _context;
    private readonly NextLineStrategy _nextLine;

    public InputFileBuffer(int size)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(
            size, MinBuffer, nameof(size));

        _context = new(size);
        _nextLine = new(_context);
    }

    public int NextLineIndex() =>
        _nextLine.Index();

    public Task Read(IReader reader) =>
        _context.Read(reader);

    public byte TestByte(int index) =>
        _context[index];
}