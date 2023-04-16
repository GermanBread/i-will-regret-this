using CunnyAPI.Definitions;
using CunnyApi.External_APIs;
using CunnyApi.Globals;
using CunnyApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CunnyAPI.Controllers;

[ApiController]
[Route("api/{uuid}/[controller]")]
public class GelbooruController : ControllerBase
{
    [HttpDelete]
    [Route("{tags}/{size:int};{skip:int}")]
    public async Task<IEnumerable<CunnyApiDatav2>> Get(string tags, int size, int skip, string uuid)
    {
        if (!BackendGlobals.UuidList.Contains(uuid))
            return Enumerable.Empty<CunnyApiDatav2>();
        BackendGlobals.UuidList.Remove(uuid);

        var data = await GetData(tags, size, skip);
        return data.Select(elm => new CunnyApiDatav2
        {
            PostUrl = $"https://gelbooru.com/index.php?page=post&s=view&id={elm.Id}",
            Tags = elm.Tags.Split(' ', StringSplitOptions.RemoveEmptyEntries),
            Source = elm.Source,
            ImageUrl = elm.FileUrl,
            ThumbnailUrl = elm.SampleUrl,
            Author = elm.Owner,
            Height = elm.Height,
            Width = elm.Width,
            Hash = elm.Md5,
            FileSize = (int)BackendGlobals.HttpClient.GetAsync(elm.FileUrl).Result.Content.Headers.ContentLength!,
            Id = elm.Id
        });
    }

    private static async Task<IEnumerable<GelbooruPostApiData>> GetData(string tags, int size, int skip)
    {
        GelbooruRequest request = new(tags);
        List<GelbooruPostApiData> data = new();

        for (int i = 0; data.Count < size + skip; i++)
        {
            if (!request.TryGetJson(i, out var raw))
            {
                return data;
            }

            // Gelbooru always responds, but sometimes with an empty collection.
            if (raw?.Post is null)
            {
                return data;
            }

            data.AddRange(raw.Value.Post);
        }

        await Task.CompletedTask;

        return data.Skip(skip).Take(size);
    }
}