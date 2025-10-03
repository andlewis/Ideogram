using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Ideogram.SDK.Exceptions;
using Ideogram.SDK.Models;

namespace Ideogram.SDK;

/// <summary>
/// Client for interacting with the Ideogram API
/// </summary>
public class IdeogramClient : IDisposable
{
    private const string BaseUrl = "https://api.ideogram.ai";
    private const string ApiVersion = "v3";
    private readonly HttpClient _httpClient;
    private readonly bool _disposeHttpClient;
    private readonly JsonSerializerOptions _jsonOptions;
    
    /// <summary>
    /// Initializes a new instance of IdeogramClient with an API key
    /// </summary>
    /// <param name="apiKey">Your Ideogram API key (required)</param>
    /// <exception cref="ArgumentException">Thrown when API key is null or empty</exception>
    public IdeogramClient(string apiKey) : this(apiKey, new HttpClient())
    {
        _disposeHttpClient = true;
    }
    
    /// <summary>
    /// Initializes a new instance of IdeogramClient with an API key and custom HttpClient
    /// </summary>
    /// <param name="apiKey">Your Ideogram API key (required)</param>
    /// <param name="httpClient">Custom HttpClient instance</param>
    /// <exception cref="ArgumentException">Thrown when API key is null or empty</exception>
    /// <exception cref="ArgumentNullException">Thrown when httpClient is null</exception>
    public IdeogramClient(string apiKey, HttpClient httpClient)
    {
        // Input validation (OWASP guideline)
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new ArgumentException("API key cannot be null or empty", nameof(apiKey));
        }
        
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _httpClient.BaseAddress = new Uri(BaseUrl);
        
        // Secure header handling (OWASP guideline)
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };
    }
    
    /// <summary>
    /// Generate images from a text prompt
    /// </summary>
    /// <param name="request">Generation request parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Generated images response</returns>
    /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
    /// <exception cref="IdeogramException">Thrown when the API request fails</exception>
    public async Task<ImageResponse> GenerateAsync(GenerateImageRequest request, CancellationToken cancellationToken = default)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }
        
        ValidatePrompt(request.Prompt);
        
        var json = JsonSerializer.Serialize(new { image_request = request }, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        return await SendRequestAsync<ImageResponse>($"/{ApiVersion}/generate", content, cancellationToken);
    }
    
    /// <summary>
    /// Edit an image with text prompts
    /// </summary>
    /// <param name="request">Edit request parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Edited images response</returns>
    /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
    /// <exception cref="IdeogramException">Thrown when the API request fails</exception>
    public async Task<ImageResponse> EditAsync(EditImageRequest request, CancellationToken cancellationToken = default)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }
        
        ValidatePrompt(request.Prompt);
        ValidateImageStream(request.ImageFile, nameof(request.ImageFile));
        
        using var content = new MultipartFormDataContent();
        
        // Add image file
        var imageContent = new StreamContent(request.ImageFile);
        imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
        content.Add(imageContent, "image_file", request.ImageFileName);
        
        // Add mask if provided
        if (request.MaskFile != null)
        {
            ValidateImageStream(request.MaskFile, nameof(request.MaskFile));
            var maskContent = new StreamContent(request.MaskFile);
            maskContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            content.Add(maskContent, "mask", request.MaskFileName ?? "mask.png");
        }
        
        // Add other parameters as JSON
        var imageRequest = new
        {
            prompt = request.Prompt,
            model = request.Model,
            magic_prompt_option = request.MagicPromptOption,
            seed = request.Seed,
            style_type = request.StyleType
        };
        
        var jsonContent = new StringContent(
            JsonSerializer.Serialize(new { image_request = imageRequest }, _jsonOptions),
            Encoding.UTF8,
            "application/json"
        );
        content.Add(jsonContent, "image_request");
        
        return await SendRequestAsync<ImageResponse>($"/{ApiVersion}/edit", content, cancellationToken);
    }
    
    /// <summary>
    /// Remix/transform an existing image
    /// </summary>
    /// <param name="request">Remix request parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Remixed images response</returns>
    /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
    /// <exception cref="IdeogramException">Thrown when the API request fails</exception>
    public async Task<ImageResponse> RemixAsync(RemixImageRequest request, CancellationToken cancellationToken = default)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }
        
        ValidatePrompt(request.Prompt);
        ValidateImageStream(request.ImageFile, nameof(request.ImageFile));
        
        using var content = new MultipartFormDataContent();
        
        // Add image file
        var imageContent = new StreamContent(request.ImageFile);
        imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
        content.Add(imageContent, "image_file", request.ImageFileName);
        
        // Add other parameters as JSON
        var imageRequest = new
        {
            prompt = request.Prompt,
            image_weight = request.ImageWeight,
            model = request.Model,
            magic_prompt_option = request.MagicPromptOption,
            seed = request.Seed,
            style_type = request.StyleType
        };
        
        var jsonContent = new StringContent(
            JsonSerializer.Serialize(new { image_request = imageRequest }, _jsonOptions),
            Encoding.UTF8,
            "application/json"
        );
        content.Add(jsonContent, "image_request");
        
        return await SendRequestAsync<ImageResponse>($"/{ApiVersion}/remix", content, cancellationToken);
    }
    
    /// <summary>
    /// Describe/analyze an image
    /// </summary>
    /// <param name="request">Describe request parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Image description response</returns>
    /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
    /// <exception cref="IdeogramException">Thrown when the API request fails</exception>
    public async Task<DescribeResponse> DescribeAsync(DescribeImageRequest request, CancellationToken cancellationToken = default)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }
        
        ValidateImageStream(request.ImageFile, nameof(request.ImageFile));
        
        using var content = new MultipartFormDataContent();
        
        // Add image file
        var imageContent = new StreamContent(request.ImageFile);
        imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
        content.Add(imageContent, "image_file", request.ImageFileName);
        
        return await SendRequestAsync<DescribeResponse>($"/{ApiVersion}/describe", content, cancellationToken);
    }
    
    private async Task<T> SendRequestAsync<T>(string endpoint, HttpContent content, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _httpClient.PostAsync(endpoint, content, cancellationToken);
            
            if (!response.IsSuccessStatusCode)
            {
                await HandleErrorResponseAsync(response);
            }
            
            var result = await response.Content.ReadFromJsonAsync<T>(_jsonOptions, cancellationToken);
            
            if (result == null)
            {
                throw new IdeogramException("Failed to deserialize response");
            }
            
            return result;
        }
        catch (HttpRequestException ex)
        {
            throw new IdeogramException("Network error occurred while communicating with Ideogram API", ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new IdeogramException("Request timed out", ex);
        }
        catch (JsonException ex)
        {
            throw new IdeogramException("Failed to parse API response", ex);
        }
    }
    
    private static async Task HandleErrorResponseAsync(HttpResponseMessage response)
    {
        var statusCode = (int)response.StatusCode;
        string errorMessage;
        string? errorCode = null;
        
        try
        {
            var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            errorMessage = errorResponse?.Error ?? errorResponse?.Message ?? "Unknown error";
            errorCode = errorResponse?.Code;
        }
        catch
        {
            errorMessage = $"HTTP {statusCode}: {response.ReasonPhrase}";
        }
        
        throw new IdeogramException(errorMessage, statusCode, errorCode);
    }
    
    // Input validation methods (OWASP guideline)
    private static void ValidatePrompt(string prompt)
    {
        if (string.IsNullOrWhiteSpace(prompt))
        {
            throw new ArgumentException("Prompt cannot be null or empty", nameof(prompt));
        }
        
        // Limit prompt length for safety
        if (prompt.Length > 10000)
        {
            throw new ArgumentException("Prompt exceeds maximum length of 10000 characters", nameof(prompt));
        }
    }
    
    private static void ValidateImageStream(Stream? stream, string paramName)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(paramName, "Image stream cannot be null");
        }
        
        if (!stream.CanRead)
        {
            throw new ArgumentException("Image stream must be readable", paramName);
        }
    }
    
    /// <summary>
    /// Disposes the client and underlying HttpClient if it was created by this instance
    /// </summary>
    public void Dispose()
    {
        if (_disposeHttpClient)
        {
            _httpClient?.Dispose();
        }
        
        GC.SuppressFinalize(this);
    }
}
