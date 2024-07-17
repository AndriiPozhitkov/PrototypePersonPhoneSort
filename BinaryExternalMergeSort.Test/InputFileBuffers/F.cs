using BinaryExternalMergeSort.InputFileBuffers;

namespace BinaryExternalMergeSort.Test.InputFileBuffers;

internal static class F
{
    internal static Context Context(string buffer)
    {
        var source = buffer.Select(a => (byte)a).ToArray();
        var context = new Context(source.Length);
        var destination = context.Buffer;
        Array.Copy(source, destination, destination.Length);
        context.Size = destination.Length;
        return context;
    }
}