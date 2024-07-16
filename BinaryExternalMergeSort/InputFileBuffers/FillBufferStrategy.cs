namespace BinaryExternalMergeSort.InputFileBuffers;

public sealed class FillBufferStrategy(Context context)
{
    private int _offset;

    public async Task Read(IReader reader)
    {
        CopyPartialLineToStart();
        await FillBuffer(reader);
    }

    private void CopyPartialLineToStart()
    {
        var buffer = context.Buffer;
        var lastLineBegin = context.LastLineBegin;
        if (0 < lastLineBegin && lastLineBegin < buffer.Length)
        {
            _offset = buffer.Length - lastLineBegin;
            Array.Copy(buffer, lastLineBegin, buffer, 0, _offset);
        }
        else
        {
            _offset = 0;
        }
    }

    private async Task FillBuffer(IReader reader)
    {
        var buffer = context.Buffer;
        var readCount = buffer.Length - _offset;
        var readed = await reader.Read(buffer, _offset, readCount);
        context.Size = _offset + readed;
    }
}