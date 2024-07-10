namespace ExternalMergeSort;

public sealed class FileReader : IReader, IDisposable
{
    private readonly StreamReader _reader;

    public FileReader(FileInfo file) =>
        _reader = new(file.FullName);

    public void Dispose() =>
        _reader.Dispose();

    public Task<string?> ReadLine() =>
        _reader.ReadLineAsync();
}