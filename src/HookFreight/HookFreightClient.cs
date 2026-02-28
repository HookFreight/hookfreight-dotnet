using HookFreight.Internal;
using HookFreight.Services;

namespace HookFreight;

public sealed class HookFreightClient
{
    public AppsService Apps { get; }
    public EndpointsService Endpoints { get; }
    public EventsService Events { get; }
    public DeliveriesService Deliveries { get; }

    public HookFreightClient(HookFreightConfig? config = null)
    {
        var http = new HFHttpClient(config ?? new HookFreightConfig());
        Apps = new AppsService(http);
        Endpoints = new EndpointsService(http);
        Events = new EventsService(http);
        Deliveries = new DeliveriesService(http);
    }
}
