namespace ExternalMergeSort;

public sealed class OsTempFileFactory : ITempFileFactory
{
    public FileInfo TempFile() =>
        new (Path.GetTempFileName());
}