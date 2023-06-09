﻿using System.Text.Json.Serialization;

namespace CunnyApi.External_APIs;

public struct LolibooruApiData
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("tags")]
    public string Tags { get; set; }

    [JsonPropertyName("created_at")]
    public int CreatedAt { get; set; }

    [JsonPropertyName("creator_id")]
    public int CreatorId { get; set; }

    [JsonPropertyName("author")]
    public string Author { get; set; }

    [JsonPropertyName("change")]
    public int Change { get; set; }

    [JsonPropertyName("source")]
    public string Source { get; set; }

    [JsonPropertyName("score")]
    public string Score { get; set; }

    [JsonPropertyName("md5")]
    public string Md5 { get; set; }

    [JsonPropertyName("file_size")]
    public int FileSize { get; set; }

    [JsonPropertyName("file_url")]
    public string FileUrl { get; set; }

    [JsonPropertyName("is_shown_in_index")]
    public bool IsShownInIndex { get; set; }

    [JsonPropertyName("preview_url")]
    public string PreviewUrl { get; set; }

    [JsonPropertyName("preview_width")]
    public int PreviewWidth { get; set; }

    [JsonPropertyName("preview_height")]
    public int PreviewHeight { get; set; }

    [JsonPropertyName("actual_preview_width")]
    public int ActualPreviewWidth { get; set; }

    [JsonPropertyName("actual_preview_height")]
    public int ActualPreviewHeight { get; set; }

    [JsonPropertyName("sample_url")]
    public string SampleUrl { get; set; }

    [JsonPropertyName("sample_width")]
    public int SampleWidth { get; set; }

    [JsonPropertyName("sample_height")]
    public int SampleHeight { get; set; }

    [JsonPropertyName("sample_file_size")]
    public int SampleFileSize { get; set; }

    [JsonPropertyName("jpeg_url")]
    public string JpegUrl { get; set; }

    [JsonPropertyName("jpeg_width")]
    public int JpegWidth { get; set; }

    [JsonPropertyName("jpeg_height")]
    public int JpegHeight { get; set; }

    [JsonPropertyName("jpeg_file_size")]
    public int JpegFileSize { get; set; }

    [JsonPropertyName("rating")]
    public string Rating { get; set; }

    [JsonPropertyName("has_children")]
    public bool HasChildren { get; set; }

    [JsonPropertyName("parent_id")]
    public int? ParentId { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("width")]
    public int Width { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }

    [JsonPropertyName("is_held")]
    public bool IsHeld { get; set; }

    [JsonPropertyName("frames_pending_string")]
    public string FramesPendingString { get; set; }

    [JsonPropertyName("frames_pending")]
    public List<object> FramesPending { get; set; }

    [JsonPropertyName("frames_string")]
    public string FramesString { get; set; }

    [JsonPropertyName("frames")]
    public List<object> Frames { get; set; }
}