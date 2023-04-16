using System.Text;

using CunnyApi.External_APIs;

namespace CunnyApi.Requests;

public class AiBooruRequest : BaseBooruRequest
{
    public AiBooruRequest(string tags)
    {
        StringBuilder sb = new();
        sb.Append("https://aibooru.online/post/index.json?");
        sb.Append("&tags=");
        sb.Append(tags.Replace(' ', '+'));
        _constructedUrl = sb.ToString();
    }

    public bool TryGetJson(int page, out IEnumerable<DanbooruApiLikeData>? result) => InternalTryGetJSON($"{_constructedUrl}&page={page}", out result);

    private readonly string _constructedUrl;
}