using System.Text.Json.Serialization;

namespace Ideogram.SDK.Models;

/// <summary>
/// Ideogram model versions
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Model
{
    /// <summary>V2 model</summary>
    [JsonPropertyName("V_2")]
    V_2,
    
    /// <summary>V2 Turbo model (faster generation)</summary>
    [JsonPropertyName("V_2_TURBO")]
    V_2_Turbo,
    
    /// <summary>V1 model</summary>
    [JsonPropertyName("V_1")]
    V_1,
    
    /// <summary>V1 Turbo model (faster generation)</summary>
    [JsonPropertyName("V_1_TURBO")]
    V_1_Turbo
}
