namespace BinaryExternalMergeSort.InputFileBuffers;

public sealed class NextLineStrategy(Context context)
{
    private const byte CR = 0x0D; // 13 \r
    private const byte LF = 0x0A; // 10 \n
    private const int EOL = -1;

    private int _bufferItemIndex;
    private int _expectedReadNumber;
    private State _state;

    private enum State
    {
        None,
        Line,
        SeekEOL,
        EOL
    }

    public int Index()
    {
        CheckReadNumber();
        return ScanLineBegin();
    }

    private void CheckReadNumber()
    {
        if (_expectedReadNumber != context.ReadNumber)
        {
            _state = State.None;
            _expectedReadNumber = context.ReadNumber;
        }
    }

    private int ScanLineBegin() => _state switch
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
            _bufferItemIndex = 0;
            context.LastLineBegin = 0;
            return 0;
        }
        else
        {
            context.LastLineBegin = 0;
            return EOL;
        }
    }

    private int Line()
    {
        var buffer = context.Buffer;
        _state = State.SeekEOL;
        for (; _bufferItemIndex < context.Size; _bufferItemIndex++)
        {
            var symbol = buffer[_bufferItemIndex];
            var symbolIsEOL = symbol == CR || symbol == LF;
            if (_state == State.SeekEOL && symbolIsEOL)
            {
                _state = State.EOL;
            }
            else if (_state == State.EOL && !symbolIsEOL)
            {
                _state = State.Line;
                context.LastLineBegin = _bufferItemIndex;
                return _bufferItemIndex;
            }
        }
        return EOL;
    }
}