using System.Text;

using CunnyApi.External_APIs;

namespace CunnyApi.Requests;

public class SafebooruRequest : BaseBooruRequest
{
    public SafebooruRequest(string tags)
    {
        StringBuilder sb = new();
        sb.Append("https://safebooru.org/index.php?page=dapi&s=post&q=index&json=1");
        sb.Append("&tags=");
        sb.Append(tags.Replace(' ', '+'));
        _constructedUrl = sb.ToString();
    }

    public bool TryGetJson(int page, out IEnumerable<SafebooruApiData>? result) => InternalTryGetJSON($"{_constructedUrl}&pid={page}", out result);

    private readonly string _constructedUrl;
}