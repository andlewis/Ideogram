using System.Text.Json.Serialization;

namespace Ideogram.SDK.Models;

/// <summary>
/// Request to generate images from a text prompt
/// </summary>
public class GenerateImageRequest
{
    /// <summary>
    /// The text prompt describing the desired image (required)
    /// </summary>
    [JsonPropertyName("prompt")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Prompt { get; set; }
    
    /// <summary>
    /// Aspect ratio for the generated image
    /// </summary>
    [JsonPropertyName("aspect_ratio")]
    public AspectRatio? AspectRatio { get; set; }
    
    /// <summary>
    /// Model to use for generation
    /// </summary>
    [JsonPropertyName("model")]
    public Model? Model { get; set; }
    
    /// <summary>
    /// Magic prompt enhancement option
    /// </summary>
    [JsonPropertyName("magic_prompt_option")]
    public MagicPromptOption? MagicPromptOption { get; set; }
    
    /// <summary>
    /// Seed for reproducible generation
    /// </summary>
    [JsonPropertyName("seed")]
    public int? Seed { get; set; }
    
    /// <summary>
    /// Style type for the generated image
    /// </summary>
    [JsonPropertyName("style_type")]
    public StyleType? StyleType { get; set; }
    
    /// <summary>
    /// Negative prompt to exclude from generation
    /// </summary>
    [JsonPropertyName("negative_prompt")]
    public string? NegativePrompt { get; set; }
    
    /// <summary>
    /// Number of images to generate (1-8)
    /// </summary>
    [JsonPropertyName("num_images")]
    public int? NumImages { get; set; }
    
    /// <summary>
    /// Color palette configuration
    /// </summary>
    [JsonPropertyName("color_palette")]
    public ColorPalette? ColorPalette { get; set; }
}
