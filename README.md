# hookfreight-dotnet

Official .NET SDK for [HookFreight](https://hookfreight.com).

## Installation

```bash
dotnet add package HookFreight
```

## Quick Start

```csharp
using HookFreight;

var client = new HookFreightClient(new HookFreightConfig
{
    ApiKey = "hf_sk_..."
});

var deliveries = await client.Deliveries.ListAsync(new Dictionary<string, object?>
{
    ["limit"] = 10
});

Console.WriteLine(deliveries);
```

## Features

- Apps: list/create/get/update/delete
- Endpoints: list/create/get/update/delete
- Events: list/get/listByEndpoint/replay
- Deliveries: list/listByEvent/retry/queueStats
- API and connection exception hierarchy

## License

Apache-2.0
