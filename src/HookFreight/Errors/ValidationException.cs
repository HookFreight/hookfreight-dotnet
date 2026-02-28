using System.Text.Json.Nodes;

namespace Hookfreight.Errors;

public sealed class ValidationException : APIException
{
    public JsonArray Errors { get; }

    public ValidationException(JsonNode? body)
        : base(400, body)
    {
        Errors = body?["errors"] as JsonArray ?? [];
    }
}
