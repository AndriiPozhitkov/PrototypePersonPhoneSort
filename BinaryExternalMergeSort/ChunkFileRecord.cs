namespace BinaryExternalMergeSort;

public sealed class ChunkFileRecord(IRecordsPoolBuffer buffer)
{
    private readonly IRecordsPoolBuffer _buffer = buffer;
    private readonly Record _record = new();

    private State _state;

    private enum State
    {
        None,
        RecordFound,
        EndOfFile
    }

    public int CompareTo(ChunkFileRecord y) =>
        _buffer.Compare0(_record, y._buffer, y._record);

    public bool IsReaded() => _state == State.RecordFound;

    public Task TryRead(IReader reader) => _state switch
    {
        State.None => None(reader),
        State.RecordFound => RecordFound(reader),
        State.EndOfFile => EndOfFile(),
        _ => Default()
    };

    private async Task None(IReader reader)
    {
        while (await _buffer.Read(reader))
        {
            if (_buffer.ScanNextRecord())
            {
                DoRecordFound();
                return;
            }
        }
        DoEndOfFile();
    }

    private void DoRecordFound()
    {
        _state = State.RecordFound;
        _record.SetBegin(_buffer);
    }

    private void DoEndOfFile()
    {
        _state = State.EndOfFile;
    }

    private async Task RecordFound(IReader reader)
    {
        if (_buffer.ScanNextRecord())
        {
            DoRecordFound();
            return;
        }
        else
        {
            await DoNone(reader);
        }
    }

    private async Task DoNone(IReader reader)
    {
        _state = State.None;
        await None(reader);
    }

    private static Task EndOfFile() => Task.CompletedTask;

    private static Task Default() => Task.CompletedTask;

    public Task Write(IWriter output) => _buffer.Write(_record, output);
}