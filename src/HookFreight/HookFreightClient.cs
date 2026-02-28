using Hookfreight.Internal;
using Hookfreight.Services;

namespace Hookfreight;

public sealed class HookfreightClient
{
    public AppsService Apps { get; }
    public EndpointsService Endpoints { get; }
    public EventsService Events { get; }
    public DeliveriesService Deliveries { get; }

    public HookfreightClient(HookfreightConfig? config = null)
    {
        var http = new HFHttpClient(config ?? new HookfreightConfig());
        Apps = new AppsService(http);
        Endpoints = new EndpointsService(http);
        Events = new EventsService(http);
        Deliveries = new DeliveriesService(http);
    }
}
