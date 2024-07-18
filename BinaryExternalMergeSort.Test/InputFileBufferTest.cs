using BinaryExternalMergeSort.InputFileBuffers;

namespace BinaryExternalMergeSort.Test;

public class InputFileBufferTest
{
    [Fact]
    public void Constructor_size_less_1_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new InputFileBuffer(0));
    }

    [Fact]
    public async Task ScanNextRecord()
    {
        using var reader = StubReader.Lines(
            "L0;F0;M0;P0",
            "L11;F11;M11;P11",
            "L222;F222;M222;P222");

        var sut = new InputFileBuffer(reader.OneAndHalfLine());

        // read 1
        Assert.True(await sut.Read(reader));
        Assert.Equal(Context.IndexNone, sut.RecordBegin());
        Assert.Equal(Context.IndexNone, sut.RecordEnd());

        // scan 1 1
        Assert.True(sut.ScanNextRecord());
        Assert.Equal(0, sut.RecordBegin());
        Assert.Equal(10, sut.RecordEnd());

        // scan 1 2
        Assert.False(sut.ScanNextRecord());
        Assert.Equal(0, sut.RecordBegin());
        Assert.Equal(10, sut.RecordEnd());

        // read 2
        Assert.True(await sut.Read(reader));
        Assert.Equal(0, sut.RecordBegin());
        Assert.Equal(10, sut.RecordEnd());

        // scan 2 1
        Assert.True(sut.ScanNextRecord());
        Assert.Equal(0, sut.RecordBegin());
        Assert.Equal(14, sut.RecordEnd());

        // scan 2 2
        Assert.False(sut.ScanNextRecord());
        Assert.Equal(0, sut.RecordBegin());
        Assert.Equal(14, sut.RecordEnd());

        // read 3
        Assert.True(await sut.Read(reader));
        Assert.Equal(0, sut.RecordBegin());
        Assert.Equal(14, sut.RecordEnd());

        // scan 3 1
        Assert.True(sut.ScanNextRecord());
        Assert.Equal(0, sut.RecordBegin());
        Assert.Equal(18, sut.RecordEnd());

        // scan 3 2
        Assert.False(sut.ScanNextRecord());
        Assert.Equal(0, sut.RecordBegin());
        Assert.Equal(18, sut.RecordEnd());

        // read 4
        Assert.False(await sut.Read(reader));
        Assert.Equal(0, sut.RecordBegin());
        Assert.Equal(18, sut.RecordEnd());

        // scan 4 1
        Assert.False(sut.ScanNextRecord());
        Assert.Equal(Context.IndexNone, sut.RecordBegin());
        Assert.Equal(Context.IndexNone, sut.RecordEnd());
    }
}