namespace ExternalMergeSort.Test;

public class RecordsPoolTest
{
    [Fact]
    public async Task ReadChunk_chunks_readed()
    {
        var reader = new StubReader3();
        var size = new RecordsPoolSizeFixed(2);
        var sut = new RecordsPool(size);

        var chunk = await sut.ReadChunk(reader);
        Assert.True(chunk.NotEmpty());

        chunk = await sut.ReadChunk(reader);
        Assert.True(chunk.NotEmpty());

        chunk = await sut.ReadChunk(reader);
        Assert.False(chunk.NotEmpty());
    }
}