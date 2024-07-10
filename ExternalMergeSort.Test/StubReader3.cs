namespace ExternalMergeSort.Test;

internal sealed class StubReader3 : IReader
{
    private readonly string[] _lines = [
        "L0;F0;M0;P0",
        "L1;F1;M1;P1",
        "L2;F2;M2;P2"];

    private int _i = 0;

    public void Dispose()
    {
    }

    public Task<string?> ReadLine() =>
        Task.FromResult(_i < _lines.Length ? _lines[_i++] : null);
}