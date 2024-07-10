namespace ExternalMergeSort.Test;

public class RecordTest
{
    private readonly StubReader3 _reader = new();
    private readonly Record _sut = new();

    [Fact]
    public async Task TryRead_line_0_Record_readed()
    {
        Assert.True(await _sut.TryRead(_reader));
        Assert.True(_sut.Equals("L0;F0;M0;P0"));
    }

    [Fact]
    public async Task TryRead_line_1_Record_readed()
    {
        await _sut.TryRead(_reader);
        Assert.True(await _sut.TryRead(_reader));
        Assert.True(_sut.Equals("L1;F1;M1;P1"));
    }

    [Fact]
    public async Task TryRead_line_2_Record_readed()
    {
        await _sut.TryRead(_reader);
        await _sut.TryRead(_reader);
        Assert.True(await _sut.TryRead(_reader));
        Assert.True(_sut.Equals("L2;F2;M2;P2"));
    }

    [Fact]
    public async Task TryRead_line_3_Returned_false()
    {
        await _sut.TryRead(_reader);
        await _sut.TryRead(_reader);
        await _sut.TryRead(_reader);
        Assert.False(await _sut.TryRead(_reader));
    }
}