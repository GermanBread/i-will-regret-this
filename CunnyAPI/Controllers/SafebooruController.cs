using System.Web;
using CunnyAPI.Definitions;
using CunnyApi.External_APIs;
using CunnyApi.Globals;
using CunnyApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CunnyAPI.Controllers;

[ApiController]
[Route("api/safebooru")]
public class SafebooruController : ControllerBase
{
    [HttpGet]
    [Route("{tags}/{size}")]
    [Route("{tags}/{size};{skip}")]
    public async Task<IEnumerable<CunnyApiDatav2>> Get(string tags, int size, int skip)
    {
        var data = await GetData(tags, size, skip);
        return data.Select(elm => new CunnyApiDatav2
        {
            PostUrl = $"https://safebooru.org/index.php?page=post&s=view&id={elm.Id}",
            ImageUrl = $"https://safebooru.org/images/{elm.Directory}/{elm.Image}",
            ThumbnailUrl = elm.Sample ? $"https://safebooru.org/samples/{elm.Directory}/sample_{Path.GetFileNameWithoutExtension(elm.Image)}.jpg" : null, // not every image has a sample
            Tags = HttpUtility.HtmlDecode(elm.Tags).Split(' ', StringSplitOptions.RemoveEmptyEntries), // We use HtmlDecode to decode symbols like "&gt;_&lt;"
            Author = elm.Owner,
            Height = elm.Height,
            Width = elm.Width,
            Hash = elm.Hash,
            FileSize = (int)BackendGlobals.HttpClient
                .GetAsync($"https://safebooru.org/images/{elm.Directory}/{elm.Image}")
                .Result.Content.Headers.ContentLength!,
            Id = elm.Id
        });
    }

    private static async Task<IEnumerable<SafebooruApiData>> GetData(string tags, int size, int skip)
    {
        SafebooruRequest request = new(tags);
        List<SafebooruApiData> data = new();

        for (int i = 0; data.Count < size + skip; i++)
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