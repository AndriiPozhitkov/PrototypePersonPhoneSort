namespace BinaryExternalMergeSort;

public sealed class NextLineIndexStrategy(InputFileBufferContext context)
{
    private const byte CR = 0x0D; // 13 \r
    private const byte LF = 0x0A; // 10 \n
    private const int EOL = -1;

    private int _index;
    private NextLineState _state;

    private enum NextLineState
    {
        None,
        Line,
        SeekEOL,
        EOL
    }

    public int NextLineIndex() => _state switch
    {
        NextLineState.None => None(),
        NextLineState.Line => Line(),
        _ => EOL,
    };

    private int None()
    {
        if (context.IsReadedGtZero)
        {
            _state = NextLineState.Line;
            _index = 0;
            return _index;
        }
        return EOL;
    }

    private int Line()
    {
        _state = NextLineState.SeekEOL;
        for (; _index < context.Readed; _index++)
        {
            var item = context[_index];
            var itemIsEOL = item == CR || item == LF;
            if (_state == NextLineState.SeekEOL && itemIsEOL)
            {
                _state = NextLineState.EOL;
            }
            else if (_state == NextLineState.EOL && !itemIsEOL)
            {
                _state = NextLineState.Line;
                return _index;
            }
        }
        return EOL;
    }
}