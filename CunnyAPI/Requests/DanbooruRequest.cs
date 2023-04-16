using System.Text;

using CunnyApi.External_APIs;

namespace CunnyApi.Requests;

public class DanbooruRequest : BaseBooruRequest
{
    public DanbooruRequest(string tags)
    {
        StringBuilder sb = new();
        sb.Append("https://danbooru.donmai.us/post/index.json?");
        sb.Append("&tags=");
        sb.Append(tags.Replace(' ', '+'));
        _constructedUrl = sb.ToString();
    }

    public bool TryGetJson(int page, out IEnumerable<DanbooruApiLikeData>? result) => InternalTryGetJSON($"{_constructedUrl}&pid={page}", out result);

    private readonly string _constructedUrl;
}