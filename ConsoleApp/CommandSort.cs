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

        var chunkFileFactory = new ChunkFileFactory(
            tempFileFactory,
            readerFactory,
            writerFactory);

        var recordsPoolSize = new RecordsPoolSizeFixed(10);
        var inputFile = args.InputFile();
        var outputFile = args.OutputFile();

        using var sort = new Sort(
            chunkFileFactory,
            recordsPoolSize,
            inputFile,
            outputFile);

        await sort.Execute();

        return Status.Ok();
    }
}