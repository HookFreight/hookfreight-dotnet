namespace HookFreight.Errors;

public class HookFreightException : Exception
{
    public HookFreightException(string message)
        : base(message)
    {
    }

    public HookFreightException(string message, Exception? inner)
        : base(message, inner)
    {
    }
}
