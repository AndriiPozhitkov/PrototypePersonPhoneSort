﻿namespace BinaryExternalMergeSort;

public sealed class ReaderFactory : IReaderFactory
{
    public IReader Reader(FileInfo file) => new FileReader(file);
}