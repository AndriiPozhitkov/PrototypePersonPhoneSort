
namespace ExternalMergeSort;

public interface IWriter : IDisposable
{
    Task WriteLine(string line);
}