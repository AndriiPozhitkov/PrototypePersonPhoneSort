using BinaryExternalMergeSort;

namespace App;

public class CommandSortByteArray(Args args) : ICommand
{
    public static bool IsMy(Args args) =>
        args.FirstArgIs("sort") || args.FirstArgIsFile();

    public async Task<Status> Execute()
    {
        var bufferFactory = new RecordsPoolBufferFactory();
        var inputFile = args.InputFile();
        var outputFile = args.OutputFile();
        var readerFactory = new ReaderFactory();
        var tempFileFactory = new OsTempFileFactory();
        var trace = new Trace();
        var writerFactory = new WriterFactory();

        var chunkFileFactory = new ChunkFileFactory(
            bufferFactory,
            readerFactory,
            tempFileFactory,
            writerFactory);

        var chunksPool =
            new ChunksPoolWorkTime(trace,
            new ChunksPool(chunkFileFactory));

        var recordsPoolFactory =
            new RecordsPoolFactoryWorkTime(trace,
            new RecordsPoolFactory(bufferFactory));

        var sort =
            new SortWorkTime(trace,
            new Sort(
                chunksPool,
                inputFile,
                outputFile,
                recordsPoolFactory));

        await sort.Execute();
        trace.WriteStatistics();
        return Status.Ok();
    }
}