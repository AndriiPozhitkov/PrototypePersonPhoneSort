using System.Runtime.InteropServices;

namespace BinaryExternalMergeSort.Test;

public class RecordTest
{
    [Fact]
    public void SizeOf()
    {
        var sut = new Record();
        Assert.Equal(4, Marshal.SizeOf(sut));
    }
}