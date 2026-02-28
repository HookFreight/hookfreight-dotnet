using System.Text.Json.Nodes;
using Hookfreight.Internal;

namespace Hookfreight.Services;

public sealed class EndpointsService
{
    private readonly HFHttpClient _http;

    internal EndpointsService(HFHttpClient http)
    {
        _http = http;
    }

    public async Task<JsonNode?> ListAsync(string appId, IDictionary<string, object?>? parameters = null, CancellationToken cancellationToken = default)
        => Unwrap(await _http.GetAsync($"/apps/{appId}/endpoints", Pagination.Clamp(parameters, Pagination.MaxEndpointsLimit), cancellationToken).ConfigureAwait(false));

    public async Task<JsonNode?> CreateAsync(object parameters, CancellationToken cancellationToken = default)
        => Unwrap(await _http.PostAsync("/endpoints", parameters, cancellationToken).ConfigureAwait(false));

    public async Task<JsonNode?> GetAsync(string endpointId, CancellationToken cancellationToken = default)
        => Unwrap(await _http.GetAsync($"/endpoints/{endpointId}", cancellationToken: cancellationToken).ConfigureAwait(false));

    public async Task<JsonNode?> UpdateAsync(string endpointId, object parameters, CancellationToken cancellationToken = default)
        => Unwrap(await _http.PutAsync($"/endpoints/{endpointId}", parameters, cancellationToken).ConfigureAwait(false));

    public async Task<JsonNode?> DeleteAsync(string endpointId, CancellationToken cancellationToken = default)
        => Unwrap(await _http.DeleteAsync($"/endpoints/{endpointId}", cancellationToken).ConfigureAwait(false));

    private static JsonNode? Unwrap(JsonNode? payload) => payload?["data"] ?? payload;
}
