namespace BinaryExternalMergeSort;

public sealed class OsTempFileFactory : ITempFileFactory
{
    public FileInfo TempFile() => new(Path.GetTempFileName());
}