namespace App;

public sealed class CommandHelp : ICommand
{
    public static bool IsMy(Args args) =>
        args.FirstArgIs("--help") ||
        args.FirstArgIs("-h") ||
        args.FirstArgIs("/?") ||
        args.FirstArgIs("h") ||
        args.FirstArgIs("help");

    public Task<Status> Execute()
    {
        Console.WriteLine(Help.Text);
        return Task.FromResult(Status.Ok());
    }
}