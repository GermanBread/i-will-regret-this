using CunnyAPI.Definitions;
using CunnyApi.External_APIs;
using CunnyApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CunnyAPI.Controllers;

[ApiController]
[Route("api/yandere")]
public class YandereController : ControllerBase
{
    [HttpGet]
    [Route("{tags}/{size}")]
    [Route("{tags}/{size};{skip}")]
    public async Task<IEnumerable<CunnyApiDatav2>> Get(string tags, int size, int skip)
    {
        var data = await GetData(tags, size, skip);
        return data.Select(elm => new CunnyApiDatav2
        {
            PostUrl = $"https://yande.re/post/show/{elm.Id}",
            Tags = elm.Tags.Split(' '),
            Source = elm.Source,
            ThumbnailUrl = elm.SampleUrl,
            ImageUrl = elm.FileUrl,
            Author = elm.Author,
            Height = elm.Height,
            Width = elm.Width,
            Hash = elm.Md5,
            FileSize = elm.FileSize,
            Id = elm.Id
        });
    }

    private static async Task<IEnumerable<YandereApiData>> GetData(string tags, int size, int skip)
    {
        YandereRequest request = new(tags);
        List<YandereApiData> data = new();

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