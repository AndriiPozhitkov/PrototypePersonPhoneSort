using System.Diagnostics;

namespace BinaryExternalMergeSort;

public sealed class OsTempFileFactory : ITempFileFactory
{
    public FileInfo TempFile()
    {
        var temp = Path.GetTempFileName();
        Debug.WriteLine($"chunk: {temp}");
        return new(temp);
    }
}