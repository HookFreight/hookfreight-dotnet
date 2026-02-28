using System;
using System.Net.Http;

namespace HookFreight;

public sealed class HookFreightConfig
{
    public string? ApiKey { get; init; }
    public string BaseUrl { get; init; } = "https://api.hookfreight.com/v1";
    public TimeSpan Timeout { get; init; } = TimeSpan.FromSeconds(30);
    public HttpClient? HttpClient { get; init; }
}
