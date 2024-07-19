﻿namespace BinaryExternalMergeSort;

public sealed class WriterFactory : IWriterFactory
{
    public IWriter Writer(FileInfo file) => new FileWriter(file);
}