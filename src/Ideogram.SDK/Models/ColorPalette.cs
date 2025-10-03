using System.Text.Json.Serialization;

namespace Ideogram.SDK.Models;

/// <summary>
/// Color palette configuration for image generation
/// </summary>
public class ColorPalette
{
    /// <summary>
    /// Predefined color palette preset
    /// </summary>
    [JsonPropertyName("name")]
    public ColorPalettePreset? Preset { get; set; }
    
    /// <summary>
    /// Custom hex color values (each must be a valid hex color string, e.g., "#FF5733")
    /// </summary>
    [JsonPropertyName("members")]
    public List<ColorPaletteMember>? Members { get; set; }
}

/// <summary>
/// Individual color member in a palette
/// </summary>
public class ColorPaletteMember
{
    /// <summary>
    /// Hex color value (e.g., "#FF5733")
    /// </summary>
    [JsonPropertyName("color")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Color { get; set; }
    
    /// <summary>
    /// Weight of this color in the palette (0.0 to 1.0)
    /// </summary>
    [JsonPropertyName("weight")]
    public double? Weight { get; set; }
}
