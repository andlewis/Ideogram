using System.Text.Json.Serialization;

namespace Ideogram.SDK.Models;

/// <summary>
/// Response containing generated images
/// </summary>
public class ImageResponse
{
    /// <summary>
    /// List of generated images
    /// </summary>
    [JsonPropertyName("data")]
    public List<ImageData> Data { get; set; } = new();
    
    /// <summary>
    /// Timestamp when the response was created
    /// </summary>
    [JsonPropertyName("created")]
    public long Created { get; set; }
}

/// <summary>
/// Individual image data
/// </summary>
public class ImageData
{
    /// <summary>
    /// URL to the generated image
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
    
    /// <summary>
    /// The prompt used (may be enhanced by magic prompt)
    /// </summary>
    [JsonPropertyName("prompt")]
    public string Prompt { get; set; } = string.Empty;
    
    /// <summary>
    /// Resolution of the generated image
    /// </summary>
    [JsonPropertyName("resolution")]
    public string Resolution { get; set; } = string.Empty;
    
    /// <summary>
    /// Whether safety filters were triggered
    /// </summary>
    [JsonPropertyName("is_image_safe")]
    public bool IsImageSafe { get; set; }
    
    /// <summary>
    /// Seed used for generation
    /// </summary>
    [JsonPropertyName("seed")]
    public int? Seed { get; set; }
}
