namespace Hookfreight.Errors;

public sealed class ConnectionException : HookfreightException
{
    public ConnectionException(string message, Exception? inner)
        : base(message, inner)
    {
    }
}
