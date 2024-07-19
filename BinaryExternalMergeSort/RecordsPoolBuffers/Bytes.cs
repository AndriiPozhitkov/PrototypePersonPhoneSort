namespace BinaryExternalMergeSort.RecordsPoolBuffers;

public static class Bytes
{
    public const int CR = 0x0D; // 13 \r
    public const int LF = 0x0A; // 10 \n

    public static bool IsEOL(this byte symbol) =>
        ((int)symbol).IsEOL();

    public static bool IsEOL(this int symbol) =>
        symbol == CR || symbol == LF;

    public static bool IsNotEOL(this byte symbol) =>
        ((int)symbol).IsNotEOL();

    public static bool IsNotEOL(this int symbol) =>
        symbol != CR && symbol != LF;
}