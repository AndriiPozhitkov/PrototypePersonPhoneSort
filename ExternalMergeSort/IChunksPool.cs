﻿namespace ExternalMergeSort;

public interface IChunksPool : IDisposable
{
    Task CreateChunkFile(IChunk chunk);

    Task MergeChunks(IWriter output);
}