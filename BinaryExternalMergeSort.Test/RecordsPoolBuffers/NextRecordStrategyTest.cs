using BinaryExternalMergeSort.RecordsPoolBuffers;

namespace BinaryExternalMergeSort.Test.RecordsPoolBuffers;

public class NextRecordStrategyTest
{
    [Fact]
    public void Scan_empty_buffer()
    {
        var context = F.Context("");
        var sut = new NextRecordStrategy(context);

        Assert.False(sut.Scan());
        Assert.Equal(Context.IndexNone, context.NextRecordBegin);
        Assert.Equal(Context.IndexNone, context.RecordBegin);
        Assert.Equal(0, context.RecordSize);

        Assert.False(sut.Scan());
        Assert.Equal(Context.IndexNone, context.NextRecordBegin);
        Assert.Equal(Context.IndexNone, context.RecordBegin);
        Assert.Equal(0, context.RecordSize);
    }

    [Fact]
    public void Scan_one_char()
    {
        var context = F.Context("A");
        var sut = new NextRecordStrategy(context);

        Assert.False(sut.Scan());
        Assert.Equal(0, context.NextRecordBegin);
        Assert.Equal(0, context.RecordBegin);
        Assert.Equal(0, context.RecordSize);

        Assert.False(sut.Scan());
        Assert.Equal(0, context.NextRecordBegin);
        Assert.Equal(0, context.RecordBegin);
        Assert.Equal(0, context.RecordSize);
    }

    [Fact]
    public void Scan_one_record_CRLF()
    {
        var context = F.Context("L;F;M;P;\r\n");
        var sut = new NextRecordStrategy(context);

        Assert.True(sut.Scan());
        Assert.Equal(0, context.NextRecordBegin);
        Assert.Equal(0, context.RecordBegin);
        Assert.Equal(8, context.RecordSize);

        Assert.False(sut.Scan());
        Assert.Equal(0, context.NextRecordBegin);
        Assert.Equal(0, context.RecordBegin);
        Assert.Equal(8, context.RecordSize);
    }

    [Fact]
    public void Scan_one_record_LF()
    {
        var context = F.Context("L;F;M;P;\n");
        var sut = new NextRecordStrategy(context);

        Assert.True(sut.Scan());
        Assert.Equal(0, context.NextRecordBegin);
        Assert.Equal(0, context.RecordBegin);
        Assert.Equal(8, context.RecordSize);

        Assert.False(sut.Scan());
        Assert.Equal(0, context.NextRecordBegin);
        Assert.Equal(0, context.RecordBegin);
        Assert.Equal(8, context.RecordSize);
    }

    [Fact]
    public void Scan_starts_from_eol()
    {
        var context = F.Context("\r\n");
        var sut = new NextRecordStrategy(context);

        Assert.False(sut.Scan());
        Assert.Equal(Context.IndexNone, context.NextRecordBegin);
        Assert.Equal(Context.IndexNone, context.RecordBegin);
        Assert.Equal(0, context.RecordSize);

        Assert.False(sut.Scan());
        Assert.Equal(Context.IndexNone, context.NextRecordBegin);
        Assert.Equal(Context.IndexNone, context.RecordBegin);
        Assert.Equal(0, context.RecordSize);
    }

    [Fact]
    public void Scan_starts_from_eol_one_char()
    {
        var context = F.Context("\r\nA");
        var sut = new NextRecordStrategy(context);

        Assert.False(sut.Scan());
        Assert.Equal(2, context.NextRecordBegin);
        Assert.Equal(Context.IndexNone, context.RecordBegin);
        Assert.Equal(0, context.RecordSize);

        Assert.False(sut.Scan());
        Assert.Equal(2, context.NextRecordBegin);
        Assert.Equal(Context.IndexNone, context.RecordBegin);
        Assert.Equal(0, context.RecordSize);
    }

    [Fact]
    public void Scan_starts_from_eol_one_record()
    {
        var context = F.Context("\r\nA\r\n");
        var sut = new NextRecordStrategy(context);

        Assert.True(sut.Scan());
        Assert.Equal(2, context.NextRecordBegin);
        Assert.Equal(2, context.RecordBegin);
        Assert.Equal(1, context.RecordSize);

        Assert.False(sut.Scan());
        Assert.Equal(2, context.NextRecordBegin);
        Assert.Equal(2, context.RecordBegin);
        Assert.Equal(1, context.RecordSize);
    }

    [Fact]
    public void Scan_two_records()
    {
        var context = F.Context("R1\r\nR2\r\n");
        var sut = new NextRecordStrategy(context);

        Assert.True(sut.Scan());
        Assert.Equal(0, context.NextRecordBegin);
        Assert.Equal(0, context.RecordBegin);
        Assert.Equal(2, context.RecordSize);

        Assert.True(sut.Scan());
        Assert.Equal(4, context.NextRecordBegin);
        Assert.Equal(4, context.RecordBegin);
        Assert.Equal(2, context.RecordSize);

        Assert.False(sut.Scan());
        Assert.Equal(4, context.NextRecordBegin);
        Assert.Equal(4, context.RecordBegin);
        Assert.Equal(2, context.RecordSize);
    }
}