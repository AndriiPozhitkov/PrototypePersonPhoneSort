namespace BinaryExternalMergeSort.Test;

public class NextLineIndexStrategyTest
{
    [Fact]
    public void NextLineIndex()
    {
        var context = new InputFileBufferContext(0);
        var sut = new NextLineIndexStrategy(context);

        Assert.Equal(-1, sut.NextLineIndex());
        Assert.Equal(-1, sut.NextLineIndex());
    }

    [Fact]
    public async Task NextLineIndex_readed_line()
    {
        using var reader = new StubReaderPersons003Csv();
        var context = new InputFileBufferContext(64);
        var sut = new NextLineIndexStrategy(context);

        await context.Read(reader);
        Assert.Equal(0, sut.NextLineIndex());
        Assert.Equal(39, sut.NextLineIndex());
        Assert.Equal(-1, sut.NextLineIndex());
    }
}