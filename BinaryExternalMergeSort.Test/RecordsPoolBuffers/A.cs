using BinaryExternalMergeSort.RecordsPoolBuffers;
using Xunit.Sdk;

namespace BinaryExternalMergeSort.Test.RecordsPoolBuffers;

internal static class A
{
    internal static void Equal(string expectedBuffer, Context actualBuffer)
    {
        if (expectedBuffer.Length > actualBuffer.Buffer.Length)
        {
            throw new XunitException(
                $"Different buffer length.\r\n" +
                $"Expected: {expectedBuffer.Length}\r\n" +
                $"  Actual: {actualBuffer.Buffer.Length}");
        }

        for (var i = 0; i < expectedBuffer.Length; i++)
        {
            var expected = (byte)expectedBuffer[i];
            var actual = actualBuffer.Buffer[i];

            if (expected != actual)
            {
                var cexpected = (char)expected;
                var cactual = (char)actual;

                throw new XunitException(
                    $"Different bytes at index {i}.\r\n" +
                    $"Expected: '{cexpected}' ({expected}) (0x{expected:X2})\r\n" +
                    $"  Actual: '{cactual}' ({actual}) (0x{actual:X2})");
            }
        }
    }
}