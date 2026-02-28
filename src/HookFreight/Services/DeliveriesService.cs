using System.Text.Json.Nodes;
using HookFreight.Internal;

namespace HookFreight.Services;

public sealed class DeliveriesService
{
    private readonly HFHttpClient _http;

    internal DeliveriesService(HFHttpClient http)
    {
        _http = http;
    }

    public async Task<JsonNode?> ListAsync(IDictionary<string, object?>? parameters = null, CancellationToken cancellationToken = default)
        => Unwrap(await _http.GetAsync("/deliveries", Pagination.Clamp(parameters, Pagination.MaxDeliveriesLimit), cancellationToken).ConfigureAwait(false));

    public async Task<JsonNode?> ListByEventAsync(string eventId, IDictionary<string, object?>? parameters = null, CancellationToken cancellationToken = default)
        => Unwrap(await _http.GetAsync($"/events/{eventId}/deliveries", Pagination.Clamp(parameters, Pagination.MaxDeliveriesLimit), cancellationToken).ConfigureAwait(false));

    public async Task RetryAsync(string deliveryId, CancellationToken cancellationToken = default)
        => _ = await _http.PostAsync($"/deliveries/{deliveryId}/retry", null, cancellationToken).ConfigureAwait(false);

    public async Task<JsonNode?> QueueStatsAsync(CancellationToken cancellationToken = default)
        => Unwrap(await _http.GetAsync("/deliveries/queue/stats", cancellationToken: cancellationToken).ConfigureAwait(false));

    private static JsonNode? Unwrap(JsonNode? payload) => payload?["data"] ?? payload;
}
