using BinaryExternalMergeSort.RecordsPoolBuffers;

namespace BinaryExternalMergeSort.Test.RecordsPoolBuffers;

public class BufferStrategyTest
{
    [Fact]
    public async Task Read_buffer_half_line_Readed_half_line()
    {
        using var reader = StubReader.Lines(1);
        var context = new Context(reader.HalfLine());
        var sut = new BufferStrategy(context);

        context.NextRecordBegin = 0;
        await sut.Read(reader);

        Assert.Equal(4, context.Size);
        A.Equal("0;1;", context);
    }

    [Fact]
    public async Task Read_buffer_one_and_half_line_Readed_one_line()
    {
        using var reader = StubReader.Lines(1);
        var context = new Context(reader.OneAndHalfLine());
        var sut = new BufferStrategy(context);

        context.NextRecordBegin = 0;
        await sut.Read(reader);

        Assert.Equal(9, context.Size);
        A.Equal("0;1;2;3\r\n\0\0\0\0", context);
    }

    [Fact]
    public async Task Read_first_read_Readed_full_buffer()
    {
        using var reader = StubReader.Lines(2);
        var context = new Context(reader.OneAndHalfLine());
        var sut = new BufferStrategy(context);

        context.NextRecordBegin = 0;
        await sut.Read(reader);

        Assert.Equal(13, context.Size);
        A.Equal("0;1;2;3\r\n1;2;", context);
    }

    [Fact]
    public async Task Read_second_read_Partial_line_copied_to_start_and_line_readed()
    {
        using var reader = StubReader.Lines(2);
        var context = new Context(reader.OneAndHalfLine());
        var sut = new BufferStrategy(context);

        context.NextRecordBegin = 0;
        await sut.Read(reader);

        context.NextRecordBegin = 9;
        await sut.Read(reader);

        Assert.Equal(9, context.Size);
        A.Equal("1;2;3;4\r\n1;2;", context);
    }
}