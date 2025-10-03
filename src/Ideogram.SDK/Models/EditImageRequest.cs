using System.Text.Json.Serialization;

namespace Ideogram.SDK.Models;

/// <summary>
/// Request to edit an image with text prompts
/// </summary>
public class EditImageRequest
{
    /// <summary>
    /// The image file to edit (required)
    /// </summary>
    [JsonIgnore]
    public required Stream ImageFile { get; set; }
    
    /// <summary>
    /// Filename for the image
    /// </summary>
    [JsonIgnore]
    public string ImageFileName { get; set; } = "image.png";
    
    /// <summary>
    /// The text prompt describing desired edits (required)
    /// </summary>
    [JsonPropertyName("prompt")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Prompt { get; set; }
    
    /// <summary>
    /// Mask image defining areas to edit
    /// </summary>
    [JsonIgnore]
    public Stream? MaskFile { get; set; }
    
    /// <summary>
    /// Filename for the mask
    /// </summary>
    [JsonIgnore]
    public string? MaskFileName { get; set; }
    
    /// <summary>
    /// Model to use for editing
    /// </summary>
    [JsonPropertyName("model")]
    public Model? Model { get; set; }
    
    /// <summary>
    /// Magic prompt enhancement option
    /// </summary>
    [JsonPropertyName("magic_prompt_option")]
    public MagicPromptOption? MagicPromptOption { get; set; }
    
    /// <summary>
    /// Seed for reproducible editing
    /// </summary>
    [JsonPropertyName("seed")]
    public int? Seed { get; set; }
    
    /// <summary>
    /// Style type for the edited image
    /// </summary>
    [JsonPropertyName("style_type")]
    public StyleType? StyleType { get; set; }
}
