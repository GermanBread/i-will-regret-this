using System.Text.Json;

using CunnyApi.External_APIs;
using CunnyApi.Globals;

namespace CunnyApi.Requests;

public abstract class BaseBooruRequest
{
    protected bool InternalTryGetJSON<T>(string url, out T? result)
    {
        string? response;
        try
        {
            response = BackendGlobals.HttpClient.GetStringAsync(url).Result;
        }
        catch (HttpRequestException)
        {
            result = default;
            return false;
        }

        if (response is null)
        {
            result = default;
            return false;
        }

        T? json;
        try
        {
            json = JsonSerializer.Deserialize<T>(response);
        }
        catch (JsonException)
        {
            result = default;
            return false;
        }
        if (json is null)
        {
            result = default;
            return false;
        }
        if (!CheckJson(json))
        {
            result = default;
            return false;
        }

        result = json;
        return true;
    }

    private static bool CheckJson<T>(in T? json)
    {
        return json switch
        {
            GelbooruApiData data => data.Post.Any(),
            // IEnumerable<AiBooruApiData> data => data.Any(),
            IEnumerable<YandereApiData> data => data.Any(),
            IEnumerable<KonachanApiData> data => data.Any(),
            IEnumerable<DanbooruApiLikeData> data => data.Any(),
            IEnumerable<LolibooruApiData> data => data.Any(),
            IEnumerable<SafebooruApiData> data => data.Any(),
            _ => false
        };
    }
}