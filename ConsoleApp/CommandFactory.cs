namespace App;

public sealed class CommandFactory
{
    public static ICommand Command(Args args)
    {
        if (CommandSort.IsMy(args)) return new CommandSort(args);
        if (CommandHelp.IsMy(args)) return new CommandHelp();
        return new CommandNotFound();
    }
}