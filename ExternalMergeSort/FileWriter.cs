namespace ExternalMergeSort;

public sealed class FileWriter : IWriter, IDisposable
{
    private readonly StreamWriter _writer;

    public FileWriter(FileInfo file) =>
        _writer = new(file.FullName);

    public void Dispose() =>
        _writer.Dispose();

    public Task WriteLine(string line) =>
        _writer.WriteLineAsync(line);
}