using System.Text;

using CunnyApi.External_APIs;

namespace CunnyApi.Requests;

public class SFWAiBooruRequest : BaseBooruRequest
{
    public SFWAiBooruRequest(string tags)
    {
        StringBuilder sb = new();
        sb.Append("https://general.aibooru.online/post/index.json?a");
        sb.Append("&tags=");
        sb.Append(tags.Replace(' ', '+'));
        _constructedUrl = sb.ToString();
    }

    public bool TryGetJson(int page, out IEnumerable<DanbooruApiLikeData>? result) => InternalTryGetJSON($"{_constructedUrl}&page={page}", out result);

    private readonly string _constructedUrl;
}