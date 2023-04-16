using CunnyAPI.Definitions;
using CunnyApi.External_APIs;
using CunnyApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CunnyAPI.Controllers;

[ApiController]
[Route("api/[controller]/{uuid}")]
public class LolibooruController : ControllerBase
{
    [HttpGet]
    [Route("{tags}/{size}")]
    [Route("{tags}/{size};{skip}")]
    public async Task<IEnumerable<CunnyApiDatav2>> Get(string tags, int size, int skip, string uuid)
    {
        if (Random.Shared.Next(1, 2) == 1)
        {
            // return "😭💢";
            // return with IEnumerable<CunnyApiDatav2> type
            
        }
        var data = await GetData(tags, size, skip);
        return data.Select(elm => new CunnyApiDatav2
        {
            PostUrl = $"https://lolibooru.moe/post/show/{elm.Id}/{elm.Tags.Replace(' ', '-')}",
            Tags = elm.Tags.Split(' '),
            Source = elm.Source,
            ImageUrl = elm.FileUrl,
            ThumbnailUrl = elm.SampleUrl,
            Author = elm.Author,
            Height = elm.Height,
            Width = elm.Width,
            Hash = elm.Md5,
            FileSize = elm.FileSize,
            Id = elm.Id
        });
    }

    private static async Task<IEnumerable<LolibooruApiData>> GetData(string tags, int size, int skip)
    {
        var request = new LolibooruRequest(tags);
        List<LolibooruApiData> data = new();

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