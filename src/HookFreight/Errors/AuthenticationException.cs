using System.Text.Json.Nodes;

namespace HookFreight.Errors;

public sealed class AuthenticationException : APIException
{
    public AuthenticationException(JsonNode? body)
        : base(401, body)
    {
    }
}
