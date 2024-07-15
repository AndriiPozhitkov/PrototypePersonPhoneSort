using BinaryExternalMergeSort.InputFileBuffers;

namespace BinaryExternalMergeSort.Test.InputFileBuffers;

public class NextLineStrategyTest
{
    [Fact]
    public void Index_context_buffer_empty_Returns_EOL()
    {
        var context = new Context(0);
        var sut = new NextLineStrategy(context);

        Assert.Equal(-1, sut.Index());
        Assert.Equal(-1, sut.Index());
    }

    [Fact]
    public async Task Index_one_full_line_and_partial_line()
    {
        using var reader = StubReader.Lines(2);
        var context = new Context(reader.LineSize_1_5());
        var sut = new NextLineStrategy(context);

        await context.Read(reader);
        Assert.Equal(0, sut.Index());
        Assert.Equal(9, sut.Index());
        Assert.Equal(-1, sut.Index());
    }
}