using System.Text;

using CunnyApi.External_APIs;

namespace CunnyApi.Requests;

public class KonachanRequest : BaseBooruRequest
{
    public KonachanRequest(string tags)
    {
        StringBuilder sb = new();
        sb.Append("https://konachan.net/post.json?");
        sb.Append("&tags=");
        sb.Append(tags.Replace(' ', '+'));
        _constructedUrl = sb.ToString();
    }

    public bool TryGetJson(int page, out IEnumerable<KonachanApiData>? result) => InternalTryGetJSON($"{_constructedUrl}&page={page}", out result);

    private readonly string _constructedUrl;
}