using System.Collections.Generic;

namespace HookFreight.Internal;

internal static class Pagination
{
    public const int MaxAppsLimit = 1000;
    public const int MaxEndpointsLimit = 1000;
    public const int MaxEventsLimit = 50;
    public const int MaxDeliveriesLimit = 1000;

    public static IDictionary<string, object?>? Clamp(IDictionary<string, object?>? parameters, int maxLimit)
    {
        if (parameters is null)
        {
            return null;
        }

        var output = new Dictionary<string, object?>(parameters);

        if (output.TryGetValue("limit", out var limitValue) && limitValue is not null && int.TryParse(limitValue.ToString(), out var limit))
        {
            if (limit < 1)
            {
                limit = 1;
            }

            if (limit > maxLimit)
            {
                limit = maxLimit;
            }

            output["limit"] = limit;
        }

        if (output.TryGetValue("offset", out var offsetValue) && offsetValue is not null && int.TryParse(offsetValue.ToString(), out var offset))
        {
            if (offset < 0)
            {
                offset = 0;
            }

            output["offset"] = offset;
        }

        return output;
    }
}
