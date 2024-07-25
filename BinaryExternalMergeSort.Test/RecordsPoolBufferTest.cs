using BinaryExternalMergeSort.RecordsPoolBuffers;

namespace BinaryExternalMergeSort.Test;

public class RecordsPoolBufferTest
{
    [Fact]
    public void Constructor_size_less_1_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new RecordsPoolBuffer(0));
    }

    [Fact]
    public async Task ScanNextRecord()
    {
        using var reader = StubReader.Lines(
            "L0;F0;M0;P0",
            "L11;F11;M11;P11",
            "L222;F222;M222;P222");

        var sut = new RecordsPoolBuffer(reader.OneAndHalfLine());

        // read 1
        Assert.True(await sut.Read(reader));
        Assert.Equal(Context.IndexNone, sut.RecordBegin());
        Assert.Equal(0, sut.RecordSize());

        // scan 1 1
        Assert.True(sut.ScanNextRecord());
        Assert.Equal(0, sut.RecordBegin());
        Assert.Equal(11, sut.RecordSize());

        // scan 1 2
        Assert.False(sut.ScanNextRecord());
        Assert.Equal(0, sut.RecordBegin());
        Assert.Equal(11, sut.RecordSize());

        // read 2
        Assert.True(await sut.Read(reader));
        Assert.Equal(0, sut.RecordBegin());
        Assert.Equal(11, sut.RecordSize());

        // scan 2 1
        Assert.True(sut.ScanNextRecord());
        Assert.Equal(0, sut.RecordBegin());
        Assert.Equal(15, sut.RecordSize());

        // scan 2 2
        Assert.False(sut.ScanNextRecord());
        Assert.Equal(0, sut.RecordBegin());
        Assert.Equal(15, sut.RecordSize());

        // read 3
        Assert.True(await sut.Read(reader));
        Assert.Equal(0, sut.RecordBegin());
        Assert.Equal(15, sut.RecordSize());

        // scan 3 1
        Assert.True(sut.ScanNextRecord());
        Assert.Equal(0, sut.RecordBegin());
        Assert.Equal(19, sut.RecordSize());

        // scan 3 2
        Assert.False(sut.ScanNextRecord());
        Assert.Equal(0, sut.RecordBegin());
        Assert.Equal(19, sut.RecordSize());

        // read 4
        Assert.False(await sut.Read(reader));
        Assert.Equal(0, sut.RecordBegin());
        Assert.Equal(19, sut.RecordSize());

        // scan 4 1
        Assert.False(sut.ScanNextRecord());
        Assert.Equal(Context.IndexNone, sut.RecordBegin());
        Assert.Equal(0, sut.RecordSize());
    }

    [Fact]
    public async Task ScanNextRecord_last_line_wo_eol_buffer_eq_size()
    {
        using var reader = StubReader.LinesLastWoEOL(
            "L;F;M;P",
            "L1;F1;M1;P1");

        var sut = new RecordsPoolBuffer(reader.SameSize());

        // read 1
        Assert.True(await sut.Read(reader));

        // scan 1 1
        Assert.True(sut.ScanNextRecord());
        Assert.Equal(0, sut.RecordBegin());
        Assert.Equal(7, sut.RecordSize());

        // scan 1 2
        Assert.True(sut.ScanNextRecord());
        Assert.Equal(9, sut.RecordBegin());
        Assert.Equal(11, sut.RecordSize());

        // scan 1 3
        Assert.False(sut.ScanNextRecord());
        Assert.Equal(9, sut.RecordBegin());
        Assert.Equal(11, sut.RecordSize());
    }

    [Fact]
    public async Task ScanNextRecord_last_line_wo_eol_buffer_gt_size()
    {
        using var reader = StubReader.LinesLastWoEOL(
            "L;F;M;P",
            "L1;F1;M1;P1");

        var sut = new RecordsPoolBuffer(reader.TenLines());

        // read 1
        Assert.True(await sut.Read(reader));

        // scan 1 1
        Assert.True(sut.ScanNextRecord());
        Assert.Equal(0, sut.RecordBegin());
        Assert.Equal(7, sut.RecordSize());

        // scan 1 2
        Assert.True(sut.ScanNextRecord());
        Assert.Equal(9, sut.RecordBegin());
        Assert.Equal(11, sut.RecordSize());

        // scan 1 3
        Assert.False(sut.ScanNextRecord());
        Assert.Equal(9, sut.RecordBegin());
        Assert.Equal(11, sut.RecordSize());
    }
}