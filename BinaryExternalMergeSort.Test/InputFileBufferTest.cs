namespace BinaryExternalMergeSort.Test;

public class InputFileBufferTest
{
    private const int EndOfBuffer = -1;

    [Fact]
    public void Constructor_size_less_1_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new InputFileBuffer(0));
    }

    [Fact]
    public async Task NextLineIndex()
    {
        using var reader = StubReader.Lines(
            "L0;F0;M0;P0",
            "L11;F11;M11;P11",
            "L222;F222;M222;P222");

        var sut = new InputFileBuffer(reader.OneAndHalfLine());

        //Assert.True(await sut.Read(reader));
        //Assert.Equal(0, sut.NextLineIndex());
        //Assert.Equal(13, sut.NextLineIndex());
        //Assert.Equal(EndOfBuffer, sut.NextLineIndex());

        //Assert.True(await sut.Read(reader));
        //Assert.Equal(0, sut.NextLineIndex());
        //Assert.Equal(17, sut.NextLineIndex());
        //Assert.Equal(EndOfBuffer, sut.NextLineIndex());

        //Assert.True(await sut.Read(reader));
        //Assert.Equal(0, sut.NextLineIndex());
        //Assert.Equal(EndOfBuffer, sut.NextLineIndex());

        //Assert.False(await sut.Read(reader));
        //Assert.Equal(EndOfBuffer, sut.NextLineIndex());
    }
}