namespace App;

public class AppException(string message) : Exception(message)
{
    public static void ThrowIfKeyIsEmpty(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new AppException("Key is empty.");
    }

    public static void ThrowIfKeyLengthGreaterThanMaxLenght(string key, int maxLength)
    {
        if (key.Length > maxLength)
            throw new AppException($"Key '{key}' length {key.Length} is greater than {maxLength}.");
    }

    public static void ThrowIfKeySymbolsAreNotAsciiLetterLower(string key)
    {
        for (var i = 0; i < key.Length; i++)
        {
            var symbol = key[i];
            if (!char.IsAsciiLetterLower(symbol))
                throw new AppException($"Key '{key}' symbol '{symbol}' at index {i} is not ASCII lower letter.");
        }
    }
}