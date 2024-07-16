namespace BinaryExternalMergeSort.InputFileBuffers;

public sealed class FillBufferStrategy
{
    private readonly Context _context;

    public FillBufferStrategy(Context context)
    {
        _context = context;
    }

    public async Task Read(IReader reader)
    {
        CopyPartialLineToStart();
        _context.ReadCount = _context.Buffer.Length - _context.Offset;

        _context.Readed = await reader.Read(
            _context.Buffer,
            _context.Offset,
            _context.ReadCount);

        _context.Size = _context.Offset + _context.Readed;
    }


    private void CopyPartialLineToStart()
    {
        if (0 < _context.LastLineBegin &&
            _context.LastLineBegin < _context.Buffer.Length)
        {
            _context.Offset = _context.Buffer.Length - _context.LastLineBegin;

            Array.Copy(
                _context.Buffer,
                _context.LastLineBegin,
                _context.Buffer,
                0,
                _context.Offset);
        }
        else
        {
            _context.Offset = 0;
        }
    }
}