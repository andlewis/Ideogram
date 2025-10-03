using System.Text.Json.Serialization;

namespace Ideogram.SDK.Models;

/// <summary>
/// Style type for image generation
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum StyleType
{
    /// <summary>Auto style</summary>
    [JsonPropertyName("AUTO")]
    Auto,
    
    /// <summary>General style</summary>
    [JsonPropertyName("GENERAL")]
    General,
    
    /// <summary>Realistic style</summary>
    [JsonPropertyName("REALISTIC")]
    Realistic,
    
    /// <summary>Design style</summary>
    [JsonPropertyName("DESIGN")]
    Design,
    
    /// <summary>3D render style</summary>
    [JsonPropertyName("RENDER_3D")]
    Render_3D,
    
    /// <summary>Anime style</summary>
    [JsonPropertyName("ANIME")]
    Anime
}
