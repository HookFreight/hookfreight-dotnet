using System.Text.Json.Nodes;
using Hookfreight.Internal;

namespace Hookfreight.Services;

public sealed class EventsService
{
    private readonly HFHttpClient _http;

    internal EventsService(HFHttpClient http)
    {
        _http = http;
    }

    public async Task<JsonNode?> ListAsync(IDictionary<string, object?>? parameters = null, CancellationToken cancellationToken = default)
        => Unwrap(await _http.GetAsync("/events", Pagination.Clamp(parameters, Pagination.MaxEventsLimit), cancellationToken).ConfigureAwait(false));

    public async Task<JsonNode?> GetAsync(string eventId, CancellationToken cancellationToken = default)
        => Unwrap(await _http.GetAsync($"/events/{eventId}", cancellationToken: cancellationToken).ConfigureAwait(false));

    public async Task<JsonNode?> ListByEndpointAsync(string endpointId, IDictionary<string, object?>? parameters = null, CancellationToken cancellationToken = default)
        => Unwrap(await _http.GetAsync($"/endpoints/{endpointId}/events", Pagination.Clamp(parameters, Pagination.MaxEventsLimit), cancellationToken).ConfigureAwait(false));

    public async Task ReplayAsync(string eventId, CancellationToken cancellationToken = default)
        => _ = await _http.PostAsync($"/events/{eventId}/replay", null, cancellationToken).ConfigureAwait(false);

    private static JsonNode? Unwrap(JsonNode? payload) => payload?["data"] ?? payload;
}
