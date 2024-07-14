namespace BinaryExternalMergeSort;

public sealed class InputFileBuffer
{
    public const int MinBuffer = 4096;

    private readonly InputFileBufferContext _context;
    private readonly NextLineIndexStrategy _nextLineIndexStrategy;

    public InputFileBuffer(int size)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(
            size, MinBuffer, nameof(size));

        _context = new(size);
        _nextLineIndexStrategy = new(_context);
    }

    public int NextLineIndex() =>
        _nextLineIndexStrategy.NextLineIndex();

    public Task Read(IReader reader) =>
        _context.Read(reader);

    public byte TestByte(int index) =>
        _context.TestByte(index);
}