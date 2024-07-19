using BinaryExternalMergeSort.RecordsPoolBuffers;

namespace BinaryExternalMergeSort.Test.RecordsPoolBuffers;

internal static class F
{
    internal static byte[] Buffer(string buffer)
    {
        var destination = new byte[buffer.Length];

        for (var i = 0; i < buffer.Length; i++)
            destination[i] = (byte)buffer[i];

        return destination;
    }

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