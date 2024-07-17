namespace BinaryExternalMergeSort.InputFileBuffers;

public sealed class NextRecordStrategy(Context context)
{
    private const byte CR = 0x0D; // 13 \r
    private const byte LF = 0x0A; // 10 \n

    private int _bufferSymbolIndex;
    private int _expectedReadNumber;
    private State _state;

    private enum State
    {
        None,
        SeekEndOfLine,
        EndOfLine
    }

    public bool Scan()
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

    private bool ScanLineBegin() => _state switch
    {
        State.None => None(),
        State.SeekEndOfLine => SeekEndOfLine(),
        State.EndOfLine => EndOfLine(),
        _ => false
    };

    private bool None() => context.Size > 0
        ? None_Filled_Buffer()
        : None_Empty_Buffer();

    private bool None_Filled_Buffer()
    {
        _bufferSymbolIndex = 1;
        var symbol = context.Buffer[0];
        if (IsEOL(symbol))
        {
            _state = State.EndOfLine;
            context.RecordBegin = Context.IndexNone;
            context.RecordEnd = Context.IndexNone;
            context.NextRecordBegin = Context.IndexNone;
            return EndOfLine();
        }
        else
        {
            _state = State.SeekEndOfLine;
            context.RecordBegin = 0;
            context.RecordEnd = Context.IndexNone;
            context.NextRecordBegin = 0;
            return SeekEndOfLine();
        }
    }

    private bool None_Empty_Buffer()
    {
        context.RecordBegin = Context.IndexNone;
        context.RecordEnd = Context.IndexNone;
        context.NextRecordBegin = Context.IndexNone;
        return false;
    }

    private bool SeekEndOfLine()
    {
        var buffer = context.Buffer;
        for (; _bufferSymbolIndex < context.Size; _bufferSymbolIndex++)
        {
            var symbol = buffer[_bufferSymbolIndex];
            if (IsEOL(symbol))
            {
                _state = State.EndOfLine;
                context.RecordBegin = context.NextRecordBegin;
                context.RecordEnd = _bufferSymbolIndex - 1;
                return true;
            }
        }
        return false;
    }

    private bool EndOfLine()
    {
        var buffer = context.Buffer;
        for (; _bufferSymbolIndex < context.Size; _bufferSymbolIndex++)
        {
            var symbol = buffer[_bufferSymbolIndex];
            if (IsNotEOL(symbol))
            {
                _state = State.SeekEndOfLine;
                context.NextRecordBegin = _bufferSymbolIndex;
                return SeekEndOfLine();
            }
        }
        return false;
    }

    private static bool IsEOL(byte symbol) =>
        symbol == CR || symbol == LF;

    private static bool IsNotEOL(byte symbol) =>
        symbol != CR && symbol != LF;
}