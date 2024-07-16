﻿using System.Diagnostics;

namespace BinaryExternalMergeSort.InputFileBuffers;

public sealed class Context
{
    private readonly byte[] _buffer;

    private int _count;
    private int _offset;
    private int _readed;

    public Context(int size)
    {
        Debug.Assert(size > 0);

        _buffer = new byte[size];
        _offset = 0;
        _count = _buffer.Length;
    }

    public bool IsReadedGtZero => _readed > 0;
    public int Readed => _readed;
    public byte this[int index] => _buffer[index];

    public void CopyPartialLineToStart(int partialLineBegin)
    {
        Debug.Assert(partialLineBegin >= 0);
        Debug.Assert(partialLineBegin < _buffer.Length);

        _offset = _buffer.Length - partialLineBegin;
        Array.Copy(_buffer, partialLineBegin, _buffer, 0, _offset);
    }

    public async Task Read(IReader reader) =>
        _readed = await reader.Read(_buffer, _offset, _count);
}