using BinaryExternalMergeSort.InputFileBuffers;

namespace BinaryExternalMergeSort.Test.InputFileBuffers;

public class NextLineStrategyTest
{
    [Fact]
    public void Index_buffer_empty_LastLineBegin_is_set_to_zero()
    {
        var context = new Context(0);
        var sut = new NextLineStrategy(context);

        context.LastLineBegin = 123;
        sut.Index();

        Assert.Equal(0, context.LastLineBegin);
    }

    [Fact]
    public void Index_buffer_empty_Returns_EOL()
    {
        var context = new Context(0);
        var sut = new NextLineStrategy(context);

        Assert.Equal(-1, sut.Index());
        Assert.Equal(0, context.LastLineBegin);

        Assert.Equal(-1, sut.Index());
        Assert.Equal(0, context.LastLineBegin);
    }

    [Fact]
    public async Task Index_one_full_line_and_partial_line()
    {
        using var reader = StubReader.Lines(2);
        var context = new Context(reader.OneAndHalfLine());
        var fillBuffer = new FillBufferStrategy(context);
        var sut = new NextLineStrategy(context);

        await fillBuffer.Read(reader);

        Assert.Equal(0, sut.Index());
        Assert.Equal(0, context.LastLineBegin);

        Assert.Equal(9, sut.Index());
        Assert.Equal(9, context.LastLineBegin);

        Assert.Equal(-1, sut.Index());
        Assert.Equal(9, context.LastLineBegin);

        await fillBuffer.Read(reader);

        Assert.Equal(0, sut.Index());
        Assert.Equal(0, context.LastLineBegin);

        Assert.Equal(-1, sut.Index());
        Assert.Equal(0, context.LastLineBegin);
    }
}