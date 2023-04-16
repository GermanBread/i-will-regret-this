using System.Text;

using CunnyApi.External_APIs;

namespace CunnyApi.Requests;

public class YandereRequest : BaseBooruRequest
{
    public YandereRequest(string tags)
    {
        StringBuilder sb = new();
        sb.Append("https://yande.re/post.json?");
        sb.Append("&tags=");
        sb.Append(tags.Replace(' ', '+'));
        _constructedUrl = sb.ToString();
    }

    public bool TryGetJson(int page, out IEnumerable<YandereApiData>? result) => InternalTryGetJSON($"{_constructedUrl}&page={page}", out result);

    private readonly string _constructedUrl;
}