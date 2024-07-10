namespace App;

internal class Program
{
    private static async Task<int> Main(string[] args)
    {
        try
        {
            Console.WriteLine("Person Phone Sort ver 0.1");

            var commandFactory = new CommandFactory();
            var programArgs = new Args(args);
            var command = CommandFactory.Command(programArgs);
            var status = await command.Execute();
            status.Print();
            return status.Code;
        }
        catch (Exception e)
        {
            var status = Status.Failed(e.ToString());
            status.Print();
            return status.Code;
        }
    }
}