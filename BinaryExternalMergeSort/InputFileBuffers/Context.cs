﻿namespace BinaryExternalMergeSort.InputFileBuffers;

public sealed class Context(int capacity)
{
    public const int IndexNone = -1;

    public readonly byte[] Buffer = new byte[capacity];

    public int NextRecordBegin = IndexNone;
    public int ReadNumber;
    public int RecordBegin = IndexNone;
    public int RecordEnd = IndexNone;
    public int Size;
}