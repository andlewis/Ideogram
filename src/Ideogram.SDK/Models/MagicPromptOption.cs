using System.Text.Json.Serialization;

namespace Ideogram.SDK.Models;

/// <summary>
/// Magic prompt enhancement options
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MagicPromptOption
{
    /// <summary>Enable magic prompt enhancement</summary>
    [JsonPropertyName("ON")]
    On,
    
    /// <summary>Disable magic prompt enhancement</summary>
    [JsonPropertyName("OFF")]
    Off,
    
    /// <summary>Use automatic magic prompt enhancement</summary>
    [JsonPropertyName("AUTO")]
    Auto
}
