using ExternalMergeSort;

namespace App;

public class CommandSort(Args args) : ICommand
{
    public static bool IsMy(Args args) =>
        args.FirstArgIs("sort") || args.FirstArgIsFile();

    public async Task<Status> Execute()
    {
        var tempFileFactory = new OsTempFileFactory();
        var readerFactory = new ReaderFactory();
        var writerFactory = new WriterFactory();
        var trace = new Trace();

        var chunkFileFactory = new ChunkFileFactory(
            tempFileFactory,
            readerFactory,
            writerFactory);

        var chunksPool = 
            new ChunksPoolWorkTime(trace,
            new ChunksPool(chunkFileFactory));
        
        var recordsPoolSize = new RecordsPoolSizeFixed(10);

        var recordsPool =
            new RecordsPoolWorkTime(trace,
            new RecordsPool(recordsPoolSize));

        var inputFile = args.InputFile();
        var outputFile = args.OutputFile();

        using var sort =
            new SortWorkTime(trace,
            new Sort(
                chunksPool,
                inputFile,
                outputFile,
                recordsPool));

        await sort.Execute();

        return Status.Ok();
    }
}