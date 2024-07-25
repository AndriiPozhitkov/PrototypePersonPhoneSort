
namespace BinaryExternalMergeSort;

public sealed class FileWriter(FileInfo file) : IWriter
{
    private static readonly byte[] EOL = CreateEOL();

    private readonly FileStream _writer = new(
        file.FullName,
        FileMode.Create,
        FileAccess.Write,
        FileShare.None,
        1_048_576,
        FileOptions.Asynchronous | FileOptions.SequentialScan);

    public void Dispose() => _writer.Dispose();

    public Task Write(byte[] buffer, int offset, int count) =>
        _writer.WriteAsync(buffer, offset, count);

    public Task WriteEOL() =>
        _writer.WriteAsync(EOL, 0, EOL.Length);

    private static byte[] CreateEOL()
    {
        var newLine = Environment.NewLine;
        var eol = new byte[newLine.Length];

        for (var i = 0; i < eol.Length; i++)
            eol[i] = (byte)newLine[i];

        return eol;
    }
}