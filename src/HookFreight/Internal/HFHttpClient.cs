using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using HookFreight.Errors;

namespace HookFreight.Internal;

internal sealed class HFHttpClient
{
    private const string SdkVersion = "0.1.0";
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly string? _apiKey;

    public HFHttpClient(HookFreightConfig config)
    {
        _apiKey = config.ApiKey;
        _baseUrl = config.BaseUrl.TrimEnd('/');
        _httpClient = config.HttpClient ?? new HttpClient();
        _httpClient.Timeout = config.Timeout;
        _httpClient.DefaultRequestHeaders.UserAgent.Clear();
        _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("hookfreight-dotnet", SdkVersion));
    }

    public Task<JsonNode?> GetAsync(string path, IDictionary<string, object?>? query = null, CancellationToken cancellationToken = default)
        => RequestAsync(HttpMethod.Get, path, query, null, cancellationToken);

    public Task<JsonNode?> PostAsync(string path, object? body = null, CancellationToken cancellationToken = default)
        => RequestAsync(HttpMethod.Post, path, null, body, cancellationToken);

    public Task<JsonNode?> PutAsync(string path, object? body = null, CancellationToken cancellationToken = default)
        => RequestAsync(HttpMethod.Put, path, null, body, cancellationToken);

    public Task<JsonNode?> DeleteAsync(string path, CancellationToken cancellationToken = default)
        => RequestAsync(HttpMethod.Delete, path, null, null, cancellationToken);

    private async Task<JsonNode?> RequestAsync(
        HttpMethod method,
        string path,
        IDictionary<string, object?>? query,
        object? body,
        CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(method, BuildUri(path, query));

        if (!string.IsNullOrWhiteSpace(_apiKey))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        if (body is not null)
        {
            var json = JsonSerializer.Serialize(body);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        HttpResponseMessage response;
        try
        {
            response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException)
        {
            throw new ConnectionException(ex.Message, ex);
        }

        var content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        var payload = DecodeBody(content, response.Content.Headers.ContentType?.MediaType);

        if (!response.IsSuccessStatusCode)
        {
            throw BuildError((int)response.StatusCode, payload);
        }

        return payload;
    }

    private Uri BuildUri(string path, IDictionary<string, object?>? query)
    {
        var uriBuilder = new StringBuilder();
        uriBuilder.Append(_baseUrl).Append('/').Append(path.TrimStart('/'));

        if (query is not null && query.Count > 0)
        {
            var first = true;
            foreach (var (key, value) in query)
            {
                if (value is null)
                {
                    continue;
                }

                uriBuilder.Append(first ? '?' : '&');
                first = false;
                uriBuilder.Append(Uri.EscapeDataString(key));
                uriBuilder.Append('=');
                uriBuilder.Append(Uri.EscapeDataString(value.ToString() ?? string.Empty));
            }
        }

        return new Uri(uriBuilder.ToString(), UriKind.Absolute);
    }

    private static JsonNode? DecodeBody(string body, string? contentType)
    {
        if (string.IsNullOrEmpty(body))
        {
            return new JsonObject();
        }

        if (!string.IsNullOrWhiteSpace(contentType) && contentType.Contains("application/json", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                return JsonNode.Parse(body);
            }
            catch (JsonException)
            {
                return JsonValue.Create(body);
            }
        }

        return JsonValue.Create(body);
    }

    private static APIException BuildError(int status, JsonNode? body)
    {
        return status switch
        {
            400 => new ValidationException(body),
            401 => new AuthenticationException(body),
            403 => new PermissionException(body),
            404 => new NotFoundException(body),
            _ => new APIException(status, body),
        };
    }
}
