using System.Net;
using CunnyAPI.Definitions;
using CunnyApi.External_APIs;
using CunnyApi.Globals;
using CunnyApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CunnyAPI.Controllers;

[ApiController]
[Route("api/{uuid}/[controller]")]
public class DanbooruController : ControllerBase
{
    [HttpGet]
    [Route("{tags}/{size}")]
    [Route("{tags}/{size};{skip}")]
    public async Task<IEnumerable<CunnyApiDatav2>> Get(string tags, int size, int skip)
    {
        switch (Random.Shared.Next(1, 4))
        {
            case 1:
                var bruh = typeof(GelbooruController).GetMethod("Get")?.Invoke(this, new object[] { tags, size, skip });
                return (IEnumerable<CunnyApiDatav2>)bruh!;
            case 2:
                var bruh2 = typeof(LolibooruController).GetMethod("Get")?.Invoke(this, new object[] { tags, size, skip });
                return (IEnumerable<CunnyApiDatav2>)bruh2!;
        }
        var data = await GetData(tags, size, skip);
        return ConstructThumbnailUrl(data).Select(elm => new CunnyApiDatav2
        {
            PostUrl = $"https://danbooru.donmai.us/posts/{elm.Id}",
            Tags = elm.Tags.Split(' '),
            ImageUrl = elm.FileUrl!,
            ThumbnailUrl = elm.PreviewUrl,
            Author = elm.Author,
            Height = elm.Height,
            Width = elm.Width,
            Hash = elm.Md5,
            FileSize = elm.FileSize,
            Id = elm.Id
        });
    }

    private static async Task<IEnumerable<DanbooruApiLikeData>> GetData(string tags, int size, int skip)
    {
        DanbooruRequest request = new(tags);
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

    private static IEnumerable<DanbooruApiLikeData> ConstructThumbnailUrl(IEnumerable<DanbooruApiLikeData> data)
    {
        // We have to do this because the JSON response for posts doesn't contain url we want and we have to construct it ourselves.

        // This URL Structure is from Danbooru Website (When you right click on an image)
        // original url format: https://cdn.donmai.us/original/aa/01/__character_name_drawn_by_author__md5.jpg
        // thumbnail url format: https://cdn.donmai.us/sample/aa/01/__character_name_drawn_by_author__sample-md5.jpg

        // We delete everything after "aa/01/" then do __ "character_name" _ "drawn_by" _ "author" __sample- "md5" .extension
        return data.Select(elm =>
        {
            elm.PreviewUrl = elm.PreviewUrl.Replace("preview", "sample");
            var extension = Path.GetExtension(elm.PreviewUrl);
            elm.PreviewUrl = Path.GetDirectoryName(elm.PreviewUrl)!;
            BackendGlobals.HttpClient.DefaultRequestHeaders.Add("User-Agent", "CunnyAPI");
            // The idea is that inside the tags there is a character name, we want but we don't know ahead of time which one it is. So we test all of them until we find one that works.
            var characterName = elm.Tags.Split(' ').Where(tag => tag.Contains("_("));
            foreach (var character in characterName)
            {
                var characterNameWithoutParentheses = character
                    .Replace("(", "")
                    .Replace(")", "");
                var url = $"{elm.PreviewUrl}/__{characterNameWithoutParentheses}_drawn_by_{elm.Author}__sample-{elm.Md5}{extension}";
                var response = BackendGlobals.HttpClient.GetAsync(url).Result;
                if (response.StatusCode != HttpStatusCode.OK)
                    continue;
                elm.PreviewUrl = url;
                break;
            }
            return elm;
        });
    }
}