using CunnyAPI.Definitions;
using CunnyApi.External_APIs;
using CunnyApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CunnyAPI.Controllers;

[ApiController]
[Route("api/{uuid}/[controller]")]
public sealed class AiBooruController : ControllerBase
{
    [HttpGet]
    [Route("{tags}/{size}")]
    [Route("{tags}/{size};{skip}")]
    public async Task<IEnumerable<CunnyApiDatav2>> Get(string tags, int size, int skip, string uuid)
    {
        var data = await GetData(tags, size, skip);
        return ConstructThumbnailUrl(data).Select(elm => new CunnyApiDatav2
        {
            PostUrl = $"https://aibooru.online/posts/{elm.Id}",
            ImageUrl = $"https://aibooru.online{elm.FileUrl}",
            Tags = elm.Tags.Split(' '),
            ThumbnailUrl = elm.PreviewUrl,
            Height = elm.Height,
            Width = elm.Width,
            Hash = Guid.NewGuid().ToString(),
            Author = elm.Author,
            FileSize = elm.FileSize,
            Id = elm.Id
        });
    }

    private static async Task<IEnumerable<DanbooruApiLikeData>> GetData(string tags, int size, int skip)
    {
        var request = new AiBooruRequest(tags);
        List<DanbooruApiLikeData> data = new();

        for (var i = 0; data.Count < size + skip; i++)
        {
            if (!request.TryGetJson(i, out var raw))
            {
                return data;
            }

            data.AddRange(raw!.Where(elm => elm.FileUrl is not null));
        }

        await Task.CompletedTask;

        return data.Skip(skip).Take(size);
    }

    /// <summary>
    /// Constructs the thumbnail url from the original url for AiBooru.
    /// </summary>
    public static IEnumerable<DanbooruApiLikeData> ConstructThumbnailUrl(IEnumerable<DanbooruApiLikeData> data)
    {
        // We construct the thumbnail url by simply replacing "original" with "sample", inserting "sample-" before the md5,
        // and replacing the file extension with the one from the preview url. (most of the time it's the same but not always)
        return data.Select(elm =>
        {
            var extension = Path.GetExtension(elm.PreviewUrl);
            var url = $"https://aibooru.online{elm.FileUrl}".Replace("original", "sample")
                .Replace(elm.FileUrl?.Split('.').Last()!, extension);
            var lastIndex = url.LastIndexOf('/');
            var sampleUrl = url.Insert(lastIndex + 1, "sample-");
            elm.PreviewUrl = sampleUrl;
            return elm;
        });
    }
}