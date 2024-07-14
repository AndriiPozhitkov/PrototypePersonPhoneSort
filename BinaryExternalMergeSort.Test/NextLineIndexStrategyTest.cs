namespace BinaryExternalMergeSort.Test;

public class NextLineIndexStrategyTest
{
    [Fact]
    public void NextLineIndex_context_buffer_empty_Returns_EOL()
    {
        var context = new InputFileBufferContext(0);
        var sut = new NextLineIndexStrategy(context);

        Assert.Equal(-1, sut.NextLineIndex());
        Assert.Equal(-1, sut.NextLineIndex());
    }

    [Fact]
    public async Task NextLineIndex_one_full_line_and_partial_line()
    {
        using var reader = StubReader.Lines(2);
        var context = new InputFileBufferContext(reader.LineSize_1_5());
        var sut = new NextLineIndexStrategy(context);

        await context.Read(reader);
        Assert.Equal(0, sut.NextLineIndex());
        Assert.Equal(9, sut.NextLineIndex());
        Assert.Equal(-1, sut.NextLineIndex());
    }
}