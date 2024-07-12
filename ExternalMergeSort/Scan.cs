namespace ExternalMergeSort;

public sealed class Scan
{
    private readonly ITrace _trace;
    private readonly FileInfo _inputFile;
    private readonly IReaderFactory _readerFactory;

    public Scan(
        ITrace trace,
        FileInfo inputFile,
        IReaderFactory readerFactory)
    {
        _trace = trace;
        _inputFile = inputFile;
        _readerFactory = readerFactory;
    }

    public async Task Execute()
    {
        using var scope = _trace.Scope(nameof(Scan), nameof(Execute));

        using var reader = _readerFactory.Reader(_inputFile);

        var line = await reader.ReadLine();

        if (line != null)
        {
            long count = 0;
            long minSize = line.Length;
            long maxSize = line.Length;
            long sum_avg = line.Length;

            while (line != null)
            {
                count++;

                if (line.Length < minSize)
                    minSize = line.Length;

                if (maxSize < line.Length)
                    maxSize = line.Length;

                sum_avg += line.Length;

                line = await reader.ReadLine();
            }

            sum_avg /= count;

            var inputfileSizeBytes = _inputFile.Length;
            var expectedPoolSize_min = inputfileSizeBytes / maxSize;
            var expectedPoolSize_max = inputfileSizeBytes / minSize;
            var expectedPoolSize_avg = inputfileSizeBytes / sum_avg;

            _trace.WriteLine($"count = {count}");
            _trace.WriteLine($"count = {count}");
            _trace.WriteLine($"min_size = {minSize}");
            _trace.WriteLine($"max_size = {maxSize}");
            _trace.WriteLine($"avg_size = {sum_avg}");

            _trace.WriteLine($"expected_pool_size_min = {expectedPoolSize_min}");
            _trace.WriteLine($"expected_pool_size_max = {expectedPoolSize_max}");
            _trace.WriteLine($"expected_pool_size_avg = {expectedPoolSize_avg}");
        }
    }
}