using ExternalMergeSort;

namespace App;

public class CommandScan(Args args) : ICommand
{
    public static bool IsMy(Args args) =>
        args.FirstArgIs("scan");

    public async Task<Status> Execute()
    {
        var readerFactory = new ReaderFactory();
        var trace = new Trace();
        var inputFile = args.InputFile();
        var scan = new Scan(trace, inputFile, readerFactory);
        await scan.Execute();
        trace.WriteStatistics();
        return Status.Ok();
    }
}