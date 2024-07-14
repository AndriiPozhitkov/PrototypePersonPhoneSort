namespace BinaryExternalMergeSort;

public sealed class NextLineIndexStrategy
{
    private const byte CR = 0x0D; // 13 \r
    private const byte LF = 0x0A; // 10 \n
    private const int EOL = -1;

    private readonly InputFileBufferContext _context;

    private int _index;
    private NextLineState _state;

    public NextLineIndexStrategy(InputFileBufferContext context)
    {
        _context = context;
    }

    private enum NextLineState
    {
        None,
        Line,
        SeekEOL,
        EOL
    }

    public int NextLineIndex()
    {
        switch (_state)
        {
            case NextLineState.None:
                if (_context._readed > 0)
                {
                    _state = NextLineState.Line;
                    _index = 0;
                    return _index;
                }
                break;

            case NextLineState.Line:
                _state = NextLineState.SeekEOL;
                for (; _index < _context._readed; _index++)
                {
                    var item = _context._buffer[_index];
                    var itemIsEOL = item == CR || item == LF;
                    if (_state == NextLineState.SeekEOL &&
                        itemIsEOL)
                    {
                        _state = NextLineState.EOL;
                    }
                    else if (_state == NextLineState.EOL &&
                        !itemIsEOL)
                    {
                        _state = NextLineState.Line;
                        return _index;
                    }
                }
                break;
        }

        return EOL;
    }
}