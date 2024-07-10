namespace App;

public sealed class Args
{
    private readonly string[] _args;
    private readonly Lazy<string> _firstArg;
    private readonly Lazy<string> _secondArg;
    private readonly Lazy<string> _thirdArg;
    private readonly Lazy<FileInfo> _inputFile;
    private readonly Lazy<FileInfo> _outputFile;

    public Args(string[] args)
    {
        _args = [.. args];
        _firstArg = new(() => Arg(0));
        _secondArg = new(() => Arg(1));
        _thirdArg = new(() => Arg(2));
        _inputFile = new(DefineInputFile);
        _outputFile = new(DefineOutputFile);
    }

    public bool FirstArgIs(string arg) => _firstArg.Value == arg;

    public bool FirstArgIsFile() => File.Exists(_firstArg.Value);

    public bool SecondArgIsFile() => File.Exists(_secondArg.Value);

    public FileInfo InputFile() => _inputFile.Value;

    public FileInfo OutputFile() => _outputFile.Value;

    private string Arg(int ordinal) => _args.Length > ordinal ? _args[ordinal] : "";

    private FileInfo DefineInputFile()
    {
        if (FirstArgIsFile())
        {
            return new FileInfo(_firstArg.Value);
        }
        else if (SecondArgIsFile())
        {
            return new FileInfo(_secondArg.Value);
        }
        return new FileInfo("TODO First File NOT FOUND");
    }

    private FileInfo DefineOutputFile()
    {
        if (FirstArgIsFile())
        {
            return DefineOutputFile(_firstArg.Value, _secondArg.Value);
        }
        else if (SecondArgIsFile())
        {
            return DefineOutputFile(_firstArg.Value, _thirdArg.Value);
        }
        return new FileInfo("TODO Second File NOT FOUND");
    }

    private FileInfo DefineOutputFile(string inputFile, string outputFile)
    {
        if (File.Exists(outputFile))
        {
            return new(outputFile);
        }
        else
        {
            var fileName = Path.GetFileNameWithoutExtension(inputFile);
            var ext = Path.GetExtension(inputFile);
            if (ext == "") ext = ".csv";
            var path = Path.GetDirectoryName(inputFile);
            var outputFileName = fileName + "-output" + ext;
            var outputFileFullName = path != null ? Path.Combine(path, outputFileName) : outputFileName;
            return new FileInfo(outputFileFullName);
        }
    }
}