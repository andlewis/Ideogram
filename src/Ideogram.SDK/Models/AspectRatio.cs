using System.Text.Json.Serialization;

namespace Ideogram.SDK.Models;

/// <summary>
/// Aspect ratio for image generation
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AspectRatio
{
    /// <summary>10:16 aspect ratio (portrait)</summary>
    [JsonPropertyName("ASPECT_10_16")]
    Aspect_10_16,
    
    /// <summary>16:10 aspect ratio (landscape)</summary>
    [JsonPropertyName("ASPECT_16_10")]
    Aspect_16_10,
    
    /// <summary>9:16 aspect ratio (portrait)</summary>
    [JsonPropertyName("ASPECT_9_16")]
    Aspect_9_16,
    
    /// <summary>16:9 aspect ratio (landscape)</summary>
    [JsonPropertyName("ASPECT_16_9")]
    Aspect_16_9,
    
    /// <summary>3:2 aspect ratio (landscape)</summary>
    [JsonPropertyName("ASPECT_3_2")]
    Aspect_3_2,
    
    /// <summary>2:3 aspect ratio (portrait)</summary>
    [JsonPropertyName("ASPECT_2_3")]
    Aspect_2_3,
    
    /// <summary>4:3 aspect ratio (landscape)</summary>
    [JsonPropertyName("ASPECT_4_3")]
    Aspect_4_3,
    
    /// <summary>3:4 aspect ratio (portrait)</summary>
    [JsonPropertyName("ASPECT_3_4")]
    Aspect_3_4,
    
    /// <summary>1:1 aspect ratio (square)</summary>
    [JsonPropertyName("ASPECT_1_1")]
    Aspect_1_1,
    
    /// <summary>1:3 aspect ratio (tall portrait)</summary>
    [JsonPropertyName("ASPECT_1_3")]
    Aspect_1_3,
    
    /// <summary>3:1 aspect ratio (wide landscape)</summary>
    [JsonPropertyName("ASPECT_3_1")]
    Aspect_3_1
}
