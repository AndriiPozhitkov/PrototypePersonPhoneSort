namespace App;

public sealed class CommandFactory
{
    public static ICommand Command(Args args)
    {
        if (CommandSort.IsMy(args)) return new CommandSort(args);
        if (CommandScan.IsMy(args)) return new CommandScan(args);
        if (CommandHelp.IsMy(args)) return new CommandHelp();
        return new CommandNotFound();
    }
}