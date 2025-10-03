using System.Text.Json.Serialization;

namespace Ideogram.SDK.Models;

/// <summary>
/// Response containing image description
/// </summary>
public class DescribeResponse
{
    /// <summary>
    /// List of descriptions
    /// </summary>
    [JsonPropertyName("data")]
    public List<DescriptionData> Data { get; set; } = new();
}

/// <summary>
/// Individual description data
/// </summary>
public class DescriptionData
{
    /// <summary>
    /// The generated description of the image
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
}
