using System.Text.Json.Serialization;

namespace CunnyAPI.Definitions;

public struct CunnyApiDatav2
{
    [JsonPropertyName("tags")]
    public string[] Tags { get; init; }

    [JsonPropertyName("author")]
    public string Author { get; init; }

    [JsonPropertyName("post_url")]
    public string PostUrl { get; set; }

    [JsonPropertyName("source")]
    public string Source { get; set; }

    [JsonPropertyName("image_url")]
    public string ImageUrl { get; init; }

    [JsonPropertyName("thumbnail_url")]
    public string? ThumbnailUrl { get; init; }

    [JsonPropertyName("height")]
    public int Height { get; init; }

    [JsonPropertyName("width")]
    public int Width { get; init; }

    [JsonPropertyName("hash")]
    public string Hash { get; init; }

    [JsonPropertyName("file_size")]
    public int FileSize { get; init; }

    [JsonPropertyName("id")]
    public int Id { get; init; }
}