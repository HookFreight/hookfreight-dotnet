using System.Text.Json.Nodes;
using HookFreight.Internal;

namespace HookFreight.Services;

public sealed class AppsService
{
    private readonly HFHttpClient _http;

    internal AppsService(HFHttpClient http)
    {
        _http = http;
    }

    public async Task<JsonNode?> ListAsync(IDictionary<string, object?>? parameters = null, CancellationToken cancellationToken = default)
        => Unwrap(await _http.GetAsync("/apps", Pagination.Clamp(parameters, Pagination.MaxAppsLimit), cancellationToken).ConfigureAwait(false));

    public async Task<JsonNode?> CreateAsync(object parameters, CancellationToken cancellationToken = default)
        => Unwrap(await _http.PostAsync("/apps", parameters, cancellationToken).ConfigureAwait(false));

    public async Task<JsonNode?> GetAsync(string appId, CancellationToken cancellationToken = default)
        => Unwrap(await _http.GetAsync($"/apps/{appId}", cancellationToken: cancellationToken).ConfigureAwait(false));

    public async Task<JsonNode?> UpdateAsync(string appId, object parameters, CancellationToken cancellationToken = default)
        => Unwrap(await _http.PutAsync($"/apps/{appId}", parameters, cancellationToken).ConfigureAwait(false));

    public async Task<JsonNode?> DeleteAsync(string appId, CancellationToken cancellationToken = default)
        => Unwrap(await _http.DeleteAsync($"/apps/{appId}", cancellationToken).ConfigureAwait(false));

    private static JsonNode? Unwrap(JsonNode? payload) => payload?["data"] ?? payload;
}
