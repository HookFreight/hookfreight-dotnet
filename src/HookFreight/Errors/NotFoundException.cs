using System.Text.Json.Nodes;

namespace Hookfreight.Errors;

public sealed class NotFoundException : APIException
{
    public NotFoundException(JsonNode? body)
        : base(404, body)
    {
    }
}
