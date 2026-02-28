using Hookfreight;

var client = new HookfreightClient(new HookfreightConfig
{
    ApiKey = Environment.GetEnvironmentVariable("HOOKFREIGHT_API_KEY")
});

var result = await client.Deliveries.ListAsync(new Dictionary<string, object?>
{
    ["limit"] = 10
});

Console.WriteLine(result);
