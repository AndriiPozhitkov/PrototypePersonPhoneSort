namespace BinaryExternalMergeSort.Test;

public class InputFileBufferTest
{
    [Fact]
    public void Constructor_size_less_4k_Throws()
    {
        var actual = Assert.Throws<ArgumentOutOfRangeException>(
            () => new InputFileBuffer(InputFileBuffer.MinBuffer - 1));

        Assert.Equal("size", actual.ParamName);
    }

    [Fact]
    public async Task NextLineIndex_readed_line()
    {
        using var reader = new StubReaderPersons003Csv();
        var sut = new InputFileBuffer(InputFileBuffer.MinBuffer);
        await sut.Read(reader);

        sut.NextLineIndex();
        Assert.Equal(39, sut.NextLineIndex());
    }

    public async Task Read()
    {
        //using var reader = new StubReaderPersons003Csv();
        //var sut = new InputFileBuffer(InputFileBuffer.MinBuffer);
        //await sut.Read(reader);
        //Assert.Equal(0x37, sut.TestByte(63));

    }
}