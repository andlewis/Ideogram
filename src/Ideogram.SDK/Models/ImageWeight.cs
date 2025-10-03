using System.Text.Json.Serialization;

namespace Ideogram.SDK.Models;

/// <summary>
/// Image weight configuration for image-to-image operations
/// </summary>
public class ImageWeight
{
    /// <summary>
    /// Weight of the image influence (0.0 to 1.0)
    /// </summary>
    [JsonPropertyName("weight")]
    public double Weight { get; set; } = 0.5;
}
