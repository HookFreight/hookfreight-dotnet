# hookfreight-dotnet

Official .NET SDK for [Hookfreight](https://docs.hookfreight.com).

## Installation

```bash
dotnet add package Hookfreight
```

## Quick Start

```csharp
using Hookfreight;

var client = new HookfreightClient(new HookfreightConfig
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
