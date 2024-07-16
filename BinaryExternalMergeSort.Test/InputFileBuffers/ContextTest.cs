using BinaryExternalMergeSort.InputFileBuffers;

namespace BinaryExternalMergeSort.Test.InputFileBuffers;

public class ContextTest
{
    [Fact]
    public async Task CopyPartialLineToBufferStart()
    {
        using var reader = StubReader.Lines(2);
        var sut = new Context(reader.OneAndHalfLine());
        await sut.Read(reader);
        sut.CopyPartialLineToStart(9);
        AssertEqual('1', sut, 0);
        AssertEqual(';', sut, 1);
        AssertEqual('2', sut, 2);
        AssertEqual(';', sut, 3);
    }

    [Fact]
    public async Task Read_buffer_half_line_Readed_half_line()
    {
        using var reader = StubReader.Lines(1);
        var sut = new Context(reader.HalfLine());
        await sut.Read(reader);
        Assert.Equal(4, sut.Readed);
        AssertEqual('0', sut, 0);
        AssertEqual(';', sut, 1);
        AssertEqual('1', sut, 2);
        AssertEqual(';', sut, 3);
    }

    [Fact]
    public async Task Read_buffer_one_and_half_line_Readed_one_line()
    {
        using var reader = StubReader.Lines(1);
        var sut = new Context(reader.OneAndHalfLine());
        await sut.Read(reader);
        Assert.Equal(9, sut.Readed);
        AssertEqual('0', sut, 0);
        AssertEqual(';', sut, 1);
        AssertEqual('1', sut, 2);
        AssertEqual(';', sut, 3);
        AssertEqual('2', sut, 4);
        AssertEqual(';', sut, 5);
        AssertEqual('3', sut, 6);
        AssertEqual('\r', sut, 7);
        AssertEqual('\n', sut, 8);
        AssertEqual('\0', sut, 9);
        AssertEqual('\0', sut, 10);
        AssertEqual('\0', sut, 11);
        AssertEqual('\0', sut, 12);
    }

    private static void AssertEqual(char s, Context sut, int index) =>
        Assert.Equal((byte)s, sut[index]);
}