namespace ExternalMergeSort;

public sealed class ManualTempFileFactory : ITempFileFactory
{
    private readonly DirectoryInfo _tempFilesDir;
    private readonly string _prefix;
    private int _fileNumber;

    public ManualTempFileFactory(DirectoryInfo tempFilesDir)
    {
        _tempFilesDir = tempFilesDir;
        _prefix = $"chunk_{DateTime.Now.Ticks:X}_";
    }

    public FileInfo TempFile()
    {
        _fileNumber++;

        var fullFileName = Path.Combine(
            _tempFilesDir.FullName,
            $"{_prefix}{_fileNumber:D6}.txt");

        return new(fullFileName);
    }
}