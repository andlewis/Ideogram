using System.Text.Json.Serialization;

namespace Ideogram.SDK.Models;

/// <summary>
/// Request to remix/transform an existing image
/// </summary>
public class RemixImageRequest
{
    /// <summary>
    /// The image file to remix (required)
    /// </summary>
    [JsonIgnore]
    public required Stream ImageFile { get; set; }
    
    /// <summary>
    /// Filename for the image
    /// </summary>
    [JsonIgnore]
    public string ImageFileName { get; set; } = "image.png";
    
    /// <summary>
    /// The text prompt describing desired remix (required)
    /// </summary>
    [JsonPropertyName("prompt")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Prompt { get; set; }
    
    /// <summary>
    /// Image weight configuration
    /// </summary>
    [JsonPropertyName("image_weight")]
    public ImageWeight? ImageWeight { get; set; }
    
    /// <summary>
    /// Model to use for remixing
    /// </summary>
    [JsonPropertyName("model")]
    public Model? Model { get; set; }
    
    /// <summary>
    /// Magic prompt enhancement option
    /// </summary>
    [JsonPropertyName("magic_prompt_option")]
    public MagicPromptOption? MagicPromptOption { get; set; }
    
    /// <summary>
    /// Seed for reproducible remixing
    /// </summary>
    [JsonPropertyName("seed")]
    public int? Seed { get; set; }
    
    /// <summary>
    /// Style type for the remixed image
    /// </summary>
    [JsonPropertyName("style_type")]
    public StyleType? StyleType { get; set; }
}
