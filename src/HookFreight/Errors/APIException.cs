using System.Text.Json.Nodes;

namespace Hookfreight.Errors;

public class APIException : HookfreightException
{
    public int Status { get; }
    public JsonNode? Body { get; }
    public string? ServerMessage { get; }

    public APIException(int status, JsonNode? body)
        : base(ResolveMessage(status, body))
    {
        Status = status;
        Body = body;
        ServerMessage = body?["message"]?.GetValue<string>();
    }

    private static string ResolveMessage(int status, JsonNode? body)
    {
        var message = body?["message"]?.GetValue<string>();
        return message ?? $"API request failed with status {status}";
    }
}
