namespace BinaryExternalMergeSort.InputFileBuffers;

public static class Bytes
{
    public const byte CR = 0x0D; // 13 \r
    public const byte LF = 0x0A; // 10 \n

    public static bool IsEOL(this byte symbol) =>
        symbol == CR || symbol == LF;

    public static bool IsNotEOL(this byte symbol) =>
        symbol != CR && symbol != LF;
}