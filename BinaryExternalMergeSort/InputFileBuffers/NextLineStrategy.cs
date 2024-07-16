namespace BinaryExternalMergeSort.InputFileBuffers;

public sealed class NextLineStrategy(Context context)
{
    private const byte CR = 0x0D; // 13 \r
    private const byte LF = 0x0A; // 10 \n
    private const int EOL = -1;

    private int _index;
    private State _state;

    private enum State
    {
        None,
        Line,
        SeekEOL,
        EOL
    }

    public int Index() => _state switch
    {
        State.None => None(),
        State.Line => Line(),
        _ => EOL,
    };

    private int None()
    {
        if (context.Size > 0)
        {
            _state = State.Line;
            _index = 0;
            context.LastLineBegin = _index;
            return _index;
        }
        return EOL;
    }

    private int Line()
    {
        var buffer = context.Buffer;
        _state = State.SeekEOL;
        for (; _index < context.Size; _index++)
        {
            var symbol = buffer[_index];
            var symbolIsEOL = symbol == CR || symbol == LF;
            if (_state == State.SeekEOL && symbolIsEOL)
            {
                _state = State.EOL;
            }
            else if (_state == State.EOL && !symbolIsEOL)
            {
                _state = State.Line;
                context.LastLineBegin = _index;
                return _index;
            }
        }
        return EOL;
    }
}