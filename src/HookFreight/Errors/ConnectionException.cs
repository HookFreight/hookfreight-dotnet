namespace HookFreight.Errors;

public sealed class ConnectionException : HookFreightException
{
    public ConnectionException(string message, Exception? inner)
        : base(message, inner)
    {
    }
}
