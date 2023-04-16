using CunnyAPI.Definitions;
using CunnyApi.External_APIs;
using CunnyApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CunnyAPI.Controllers;

[ApiController]
[Route("api/sfwaibooru")]
public class SFWAiBooruController : ControllerBase
{
    [HttpGet]
    [Route("{tags}/{size}")]
    [Route("{tags}/{size};{skip}")]
    public async Task<IEnumerable<CunnyApiDatav2>> Get(string tags, int size, int skip)
    {
        var data = await GetData(tags, size, skip);
        return AiBooruController.ConstructThumbnailUrl(data).Select(elm => new CunnyApiDatav2
        {
            PostUrl = $"https://general.aibooru.online/posts/{elm.Id}",
            ImageUrl = $"https://aibooru.online{elm.FileUrl}",
            Tags = elm.Tags.Split(' '),
            ThumbnailUrl = elm.PreviewUrl,
            Height = elm.Height,
            Width = elm.Width,
            Hash = elm.Md5,
            Author = elm.Author,
            FileSize = elm.FileSize,
            Id = elm.Id
        });
    }

    private static async Task<IEnumerable<DanbooruApiLikeData>> GetData(string tags, int size, int skip)
    {
        SFWAiBooruRequest request = new(tags);
        List<DanbooruApiLikeData> data = new();

        for (var i = 0; data.Count < size + skip; i++)
        {
            if (!request.TryGetJson(i, out var raw))
            {
                return data;
            }

            data.AddRange(raw!);
        }

        await Task.CompletedTask;

        return data.Skip(skip).Take(size);
    }
}