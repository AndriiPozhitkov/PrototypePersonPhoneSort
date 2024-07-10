namespace App;

public interface ICommand
{
    Task<Status> Execute();
}