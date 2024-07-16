using BinaryExternalMergeSort.InputFileBuffers;

namespace BinaryExternalMergeSort.Test.InputFileBuffers;

public class FillBufferStrategyTest
{
    [Fact]
    public async Task Read_buffer_half_line_Readed_half_line()
    {
        using var reader = StubReader.Lines(1);
        var context = new Context(reader.HalfLine());
        var sut = new FillBufferStrategy(context);

        await sut.Read(reader);

        Assert.Equal(4, context.Readed);
        Assert.Equal(4, context.Size);
        Assert.Equal('0', context.TestChar(0));
        Assert.Equal(';', context.TestChar(1));
        Assert.Equal('1', context.TestChar(2));
        Assert.Equal(';', context.TestChar(3));
    }

    [Fact]
    public async Task Read_buffer_one_and_half_line_Readed_one_line()
    {
        using var reader = StubReader.Lines(1);
        var context = new Context(reader.OneAndHalfLine());
        var sut = new FillBufferStrategy(context);

        await sut.Read(reader);

        Assert.Equal(9, context.Readed);
        Assert.Equal(9, context.Size);
        Assert.Equal('0', context.TestChar(0));
        Assert.Equal(';', context.TestChar(1));
        Assert.Equal('1', context.TestChar(2));
        Assert.Equal(';', context.TestChar(3));
        Assert.Equal('2', context.TestChar(4));
        Assert.Equal(';', context.TestChar(5));
        Assert.Equal('3', context.TestChar(6));
        Assert.Equal('\r', context.TestChar(7));
        Assert.Equal('\n', context.TestChar(8));
        Assert.Equal('\0', context.TestChar(9));
        Assert.Equal('\0', context.TestChar(10));
        Assert.Equal('\0', context.TestChar(11));
        Assert.Equal('\0', context.TestChar(12));
    }

    [Fact]
    public async Task Read_First_read_Context_readed()
    {
        using var reader = StubReader.Lines(2);
        var context = new Context(reader.OneAndHalfLine());
        var sut = new FillBufferStrategy(context);

        await sut.Read(reader);

        Assert.Equal(13, context.Readed);
        Assert.Equal('0', context.TestChar(0));
        Assert.Equal(';', context.TestChar(1));
        Assert.Equal('1', context.TestChar(2));
        Assert.Equal(';', context.TestChar(3));
        Assert.Equal('2', context.TestChar(4));
        Assert.Equal(';', context.TestChar(5));
        Assert.Equal('3', context.TestChar(6));
        Assert.Equal('\r', context.TestChar(7));
        Assert.Equal('\n', context.TestChar(8));
        Assert.Equal('1', context.TestChar(9));
        Assert.Equal(';', context.TestChar(10));
        Assert.Equal('2', context.TestChar(11));
        Assert.Equal(';', context.TestChar(12));
    }

    [Fact]
    public async Task Read_Second_read_Partial_line_copied_to_start()
    {
        using var reader = StubReader.Lines(2);
        var context = new Context(reader.OneAndHalfLine());
        var sut = new FillBufferStrategy(context);

        context.LastLineBegin = 0;
        await sut.Read(reader);
        context.LastLineBegin = 9;
        await sut.Read(reader);

        Assert.Equal(5, context.Readed);
        Assert.Equal('1', context.TestChar(0));
        Assert.Equal(';', context.TestChar(1));
        Assert.Equal('2', context.TestChar(2));
        Assert.Equal(';', context.TestChar(3));
    }
}