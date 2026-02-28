using System.Text.Json.Nodes;

namespace HookFreight.Errors;

public sealed class PermissionException : APIException
{
    public PermissionException(JsonNode? body)
        : base(403, body)
    {
    }
}
