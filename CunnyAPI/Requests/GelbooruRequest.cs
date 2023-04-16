using System.Text;

using CunnyApi.External_APIs;

namespace CunnyApi.Requests;

public class GelbooruRequest : BaseBooruRequest
{
    public GelbooruRequest(string tags)
    {
        StringBuilder sb = new();
        sb.Append("https://gelbooru.com/index.php?page=dapi&s=post&q=index&json=1");
        sb.Append("&tags=");
        sb.Append(tags.Replace(' ', '+'));
        _constructedUrl = sb.ToString();
    }

    public bool TryGetJson(int page, out GelbooruApiData? result) => InternalTryGetJSON($"{_constructedUrl}&pid={page}", out result);

    private readonly string _constructedUrl;
}