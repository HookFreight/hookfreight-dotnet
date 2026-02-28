namespace Hookfreight.Errors;

public class HookfreightException : Exception
{
    public HookfreightException(string message)
        : base(message)
    {
    }

    public HookfreightException(string message, Exception? inner)
        : base(message, inner)
    {
    }
}
