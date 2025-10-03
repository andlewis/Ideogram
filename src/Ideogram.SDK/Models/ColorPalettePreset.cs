using System.Text.Json.Serialization;

namespace Ideogram.SDK.Models;

/// <summary>
/// Color palette presets for image generation
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ColorPalettePreset
{
    /// <summary>Ember color palette</summary>
    [JsonPropertyName("EMBER")]
    Ember,
    
    /// <summary>Fresh color palette</summary>
    [JsonPropertyName("FRESH")]
    Fresh,
    
    /// <summary>Jungle color palette</summary>
    [JsonPropertyName("JUNGLE")]
    Jungle,
    
    /// <summary>Magic color palette</summary>
    [JsonPropertyName("MAGIC")]
    Magic,
    
    /// <summary>Melon color palette</summary>
    [JsonPropertyName("MELON")]
    Melon,
    
    /// <summary>Mosaic color palette</summary>
    [JsonPropertyName("MOSAIC")]
    Mosaic,
    
    /// <summary>Muted color palette</summary>
    [JsonPropertyName("MUTED")]
    Muted,
    
    /// <summary>Pastel color palette</summary>
    [JsonPropertyName("PASTEL")]
    Pastel,
    
    /// <summary>Realism color palette</summary>
    [JsonPropertyName("REALISM")]
    Realism,
    
    /// <summary>Rustic color palette</summary>
    [JsonPropertyName("RUSTIC")]
    Rustic,
    
    /// <summary>Sakura color palette</summary>
    [JsonPropertyName("SAKURA")]
    Sakura
}
