using System.Text.Json.Serialization;

namespace Ideogram.SDK.Models;

/// <summary>
/// Error response from the API
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// Error message
    /// </summary>
    [JsonPropertyName("error")]
    public string Error { get; set; } = string.Empty;
    
    /// <summary>
    /// Error code
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; set; }
    
    /// <summary>
    /// Additional error details
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}
