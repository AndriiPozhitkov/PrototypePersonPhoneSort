namespace BinaryExternalMergeSort.RecordsPoolBuffers;

public sealed class BufferStrategy(Context context)
{
    private int _offset;

    public async Task<bool> Read(IReader reader)
    {
        context.ReadNumber++;
        CopyPartialRecordToStartOfBuffer();
        return await FillBuffer(reader);
    }

    private void CopyPartialRecordToStartOfBuffer()
    {
        var buffer = context.Buffer;
        var nextRecordBegin = context.NextRecordBegin;
        if (0 < nextRecordBegin && nextRecordBegin < buffer.Length)
        {
            _offset = buffer.Length - nextRecordBegin;
            Array.Copy(buffer, nextRecordBegin, buffer, 0, _offset);
        }
        else
        {
            _offset = 0;
        }
    }

    private async Task<bool> FillBuffer(IReader reader)
    {
        var buffer = context.Buffer;
        var readCount = buffer.Length - _offset;
        var readed = await reader.Read(buffer, _offset, readCount);
        context.Size = _offset + readed;
        context.EndOfFile = reader.EndOfFile();
        return readed > 0;
    }
}