namespace BinaryExternalMergeSort;

public sealed class FileReader(FileInfo file) : IReader
{
    private readonly FileStream _reader = new(
        file.FullName,
        FileMode.Open,
        FileAccess.Read,
        FileShare.None,
        1_048_576,
        FileOptions.Asynchronous | FileOptions.SequentialScan);

    public void Dispose() => _reader.Dispose();

    public bool EndOfFile() => _reader.Length <= _reader.Position;

    public Task<int> Read(byte[] buffer, int offset, int count) =>
        _reader.ReadAsync(buffer, offset, count);
}