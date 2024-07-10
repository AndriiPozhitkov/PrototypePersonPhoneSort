namespace App;

public class Status
{
    private readonly int _code;
    private readonly string _outMessage;
    private readonly string _errorMessage;

    private Status(int code, string outMessage, string errorMessage)
    {
        _code = code;
        _outMessage = outMessage;
        _errorMessage = errorMessage;
    }

    public int Code => _code;

    public static Status Failed(string outMessage) => new(1, outMessage, "");

    public static Status Failed(Exception e) => new(1, e.Message, e.ToString());

    public static Status Ok() => new(0, "", "");

    public void Print()
    {
        if (_code > 0)
        {
            Console.Out.WriteLine(_outMessage);
            Console.Error.WriteLine(_errorMessage);
        }
    }
}