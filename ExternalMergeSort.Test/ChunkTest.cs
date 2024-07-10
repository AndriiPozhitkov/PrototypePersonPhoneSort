namespace ExternalMergeSort.Test;

public class ChunkTest
{
    [Fact]
    public async Task Write()
    {
        var writer = new SpyWriter();

        var sut = new Chunk(2, [
            new("L0;F0;M0;P0"),
            new("L1;F1;M1;P1"),
            new("L2;F2;M2;P2")]);

        await sut.Write(writer);

        Assert.Equal(2, writer.RecordsCount());
        Assert.Equal("L0;F0;M0;P0", writer.Records(0));
        Assert.Equal("L1;F1;M1;P1", writer.Records(1));
    }
}