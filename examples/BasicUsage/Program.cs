using HookFreight;

var client = new HookFreightClient(new HookFreightConfig
{
    ApiKey = Environment.GetEnvironmentVariable("HOOKFREIGHT_API_KEY")
});

var result = await client.Deliveries.ListAsync(new Dictionary<string, object?>
{
    ["limit"] = 10
});

Console.WriteLine(result);
