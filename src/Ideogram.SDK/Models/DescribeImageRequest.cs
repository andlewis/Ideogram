using System.Text.Json.Serialization;

namespace Ideogram.SDK.Models;

/// <summary>
/// Request to describe/analyze an image
/// </summary>
public class DescribeImageRequest
{
    /// <summary>
    /// The image file to describe (required)
    /// </summary>
    [JsonIgnore]
    public required Stream ImageFile { get; set; }
    
    /// <summary>
    /// Filename for the image
    /// </summary>
    [JsonIgnore]
    public string ImageFileName { get; set; } = "image.png";
}
