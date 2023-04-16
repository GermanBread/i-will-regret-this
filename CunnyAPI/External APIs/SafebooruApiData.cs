using System.Text.Json.Serialization;

namespace CunnyApi.External_APIs;

public struct SafebooruApiData
{
    [JsonPropertyName("directory")]
    public string Directory { get; set; }

    [JsonPropertyName("hash")]
    public string Hash { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("image")]
    public string Image { get; set; }

    [JsonPropertyName("change")]
    public int Change { get; set; }

    [JsonPropertyName("owner")]
    public string Owner { get; set; }

    [JsonPropertyName("parent_id")]
    public int ParentId { get; set; }

    [JsonPropertyName("rating")]
    public string Rating { get; set; }

    [JsonPropertyName("sample")]
    public bool Sample { get; set; }

    [JsonPropertyName("sample_height")]
    public int SampleHeight { get; set; }

    [JsonPropertyName("sample_width")]
    public int SampleWidth { get; set; }

    [JsonPropertyName("score")]
    public object Score { get; set; }

    [JsonPropertyName("tags")]
    public string Tags { get; set; }

    [JsonPropertyName("width")]
    public int Width { get; set; }
}