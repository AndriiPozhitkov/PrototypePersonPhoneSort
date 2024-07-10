namespace App;

public sealed class CommandNotFound : ICommand
{
    public Task<Status> Execute() => Task.FromResult(Status.Failed(
        $"Command not found.{Environment.NewLine}{Help.Text}"));
}