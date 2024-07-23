using System.Runtime.InteropServices;
using BinaryExternalMergeSort.Test.RecordsPoolBuffers;

namespace BinaryExternalMergeSort.Test;

public class RecordTest
{
    public static TheoryData<string, Record, Record, int>
    Compare_Data => new()
    {
        {"A\r\nA\r\n", new(0, 1), new(3, 1), 0},
        {"A\r\nB\r\n", new(0, 1), new(3, 1), -1},
        {"B\r\nA\r\n", new(0, 1), new(3, 1), 1},
        {"A\nA\n", new(0, 1), new(2, 1), 0},
        {"A\nB\n", new(0, 1), new(2, 1), -1},
        {"B\nA\n", new(0, 1), new(2, 1), 1},
        {"A\nA", new(0, 1), new(2, 1), 0},
        {"A\nAB\n", new(0, 1), new(2, 2), -1},
        {"A\nAB", new(0, 1), new(2, 2), -1},
        {"AB\nA\n", new(0, 2), new(3, 1), 1},
        {"AB\nA", new(0, 2), new(3, 1), 1},
    };

    [Theory]
    [MemberData(nameof(Compare_Data))]
    public void Compare(string buffer, Record x, Record y, int expected) =>
        Assert.Equal(expected, x.Compare(F.Buffer(buffer), y));

    [Fact]
    public void SizeOf()
    {
        var sut = new Record();
        Assert.Equal(8, Marshal.SizeOf(sut));
    }
}