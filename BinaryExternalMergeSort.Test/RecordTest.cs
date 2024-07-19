using System.Runtime.InteropServices;
using BinaryExternalMergeSort.Test.RecordsPoolBuffers;

namespace BinaryExternalMergeSort.Test;

public class RecordTest
{
    public static TheoryData<string, Record, Record, int>
    Compare_Data => new()
    {
        {"A\r\nA\r\n", new(0), new(3), 0},
        {"A\r\nB\r\n", new(0), new(3), -1},
        {"B\r\nA\r\n", new(0), new(3), 1},
        {"A\nA\n", new(0), new(2), 0},
        {"A\nB\n", new(0), new(2), -1},
        {"B\nA\n", new(0), new(2), 1},
        {"A\nA", new(0), new(2), 0},
        {"A\nAB\n", new(0), new(2), -1},
        {"A\nAB", new(0), new(2), -1},
        {"AB\nA\n", new(0), new(3), 1},
        {"AB\nA", new(0), new(3), 1},
    };

    [Theory]
    [MemberData(nameof(Compare_Data))]
    public void Compare(string buffer, Record x, Record y, int expected) =>
        Assert.Equal(expected, x.Compare(F.Buffer(buffer), y));

    [Fact]
    public void SizeOf()
    {
        var sut = new Record();
        Assert.Equal(4, Marshal.SizeOf(sut));
    }
}