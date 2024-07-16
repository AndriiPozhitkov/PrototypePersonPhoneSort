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
        if (context.Readed > 0)
        {
            _state = State.Line;
            _index = 0;
            return _index;
        }
        return EOL;
    }

    private int Line()
    {
        _state = State.SeekEOL;
        for (; _index < context.Readed; _index++)
        {
            var item = context.Buffer[_index];
            var itemIsEOL = item == CR || item == LF;
            if (_state == State.SeekEOL && itemIsEOL)
            {
                _state = State.EOL;
            }
            else if (_state == State.EOL && !itemIsEOL)
            {
                _state = State.Line;
                return _index;
            }
        }
        return EOL;
    }
}