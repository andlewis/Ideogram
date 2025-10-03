using System.Net;
using System.Text;
using System.Text.Json;
using Ideogram.SDK;
using Ideogram.SDK.Exceptions;
using Ideogram.SDK.Models;
using Moq;
using Moq.Protected;
using Xunit;

namespace Ideogram.SDK.Tests;

public class IdeogramClientTests
{
    private const string TestApiKey = "test-api-key-12345";
    
    [Fact]
    public void Constructor_WithValidApiKey_CreatesClient()
    {
        // Act
        using var client = new IdeogramClient(TestApiKey);
        
        // Assert
        Assert.NotNull(client);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithInvalidApiKey_ThrowsArgumentException(string? apiKey)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new IdeogramClient(apiKey!));
    }
    
    [Fact]
    public void Constructor_WithNullHttpClient_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new IdeogramClient(TestApiKey, null!));
    }
    
    [Fact]
    public async Task GenerateAsync_WithNullRequest_ThrowsArgumentNullException()
    {
        // Arrange
        using var client = new IdeogramClient(TestApiKey);
        
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => client.GenerateAsync(null!));
    }
    
    [Fact]
    public async Task GenerateAsync_WithEmptyPrompt_ThrowsArgumentException()
    {
        // Arrange
        using var client = new IdeogramClient(TestApiKey);
        var request = new GenerateImageRequest { Prompt = "" };
        
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => client.GenerateAsync(request));
    }
    
    [Fact]
    public async Task GenerateAsync_WithValidRequest_ReturnsResponse()
    {
        // Arrange
        var mockResponse = new ImageResponse
        {
            Data = new List<ImageData>
            {
                new()
                {
                    Url = "https://example.com/image.png",
                    Prompt = "A test image",
                    Resolution = "1024x1024",
                    IsImageSafe = true,
                    Seed = 12345
                }
            },
            Created = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };
        
        var mockHandler = CreateMockHttpMessageHandler(HttpStatusCode.OK, mockResponse);
        using var httpClient = new HttpClient(mockHandler.Object);
        using var client = new IdeogramClient(TestApiKey, httpClient);
        
        var request = new GenerateImageRequest
        {
            Prompt = "A beautiful sunset",
            AspectRatio = AspectRatio.Aspect_16_9
        };
        
        // Act
        var result = await client.GenerateAsync(request);
        
        // Assert
        Assert.NotNull(result);
        Assert.Single(result.Data);
        Assert.Equal("https://example.com/image.png", result.Data[0].Url);
    }
    
    [Fact]
    public async Task EditAsync_WithNullRequest_ThrowsArgumentNullException()
    {
        // Arrange
        using var client = new IdeogramClient(TestApiKey);
        
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => client.EditAsync(null!));
    }
    
    [Fact]
    public async Task EditAsync_WithValidRequest_ReturnsResponse()
    {
        // Arrange
        var mockResponse = new ImageResponse
        {
            Data = new List<ImageData>
            {
                new()
                {
                    Url = "https://example.com/edited.png",
                    Prompt = "Edit test",
                    Resolution = "1024x1024",
                    IsImageSafe = true
                }
            },
            Created = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };
        
        var mockHandler = CreateMockHttpMessageHandler(HttpStatusCode.OK, mockResponse);
        using var httpClient = new HttpClient(mockHandler.Object);
        using var client = new IdeogramClient(TestApiKey, httpClient);
        
        using var imageStream = new MemoryStream(Encoding.UTF8.GetBytes("fake image data"));
        var request = new EditImageRequest
        {
            ImageFile = imageStream,
            Prompt = "Make it blue"
        };
        
        // Act
        var result = await client.EditAsync(request);
        
        // Assert
        Assert.NotNull(result);
        Assert.Single(result.Data);
    }
    
    [Fact]
    public async Task RemixAsync_WithNullRequest_ThrowsArgumentNullException()
    {
        // Arrange
        using var client = new IdeogramClient(TestApiKey);
        
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemixAsync(null!));
    }
    
    [Fact]
    public async Task DescribeAsync_WithNullRequest_ThrowsArgumentNullException()
    {
        // Arrange
        using var client = new IdeogramClient(TestApiKey);
        
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => client.DescribeAsync(null!));
    }
    
    [Fact]
    public async Task DescribeAsync_WithValidRequest_ReturnsResponse()
    {
        // Arrange
        var mockResponse = new DescribeResponse
        {
            Data = new List<DescriptionData>
            {
                new() { Description = "A beautiful sunset over mountains" }
            }
        };
        
        var mockHandler = CreateMockHttpMessageHandler(HttpStatusCode.OK, mockResponse);
        using var httpClient = new HttpClient(mockHandler.Object);
        using var client = new IdeogramClient(TestApiKey, httpClient);
        
        using var imageStream = new MemoryStream(Encoding.UTF8.GetBytes("fake image data"));
        var request = new DescribeImageRequest
        {
            ImageFile = imageStream
        };
        
        // Act
        var result = await client.DescribeAsync(request);
        
        // Assert
        Assert.NotNull(result);
        Assert.Single(result.Data);
        Assert.Equal("A beautiful sunset over mountains", result.Data[0].Description);
    }
    
    [Fact]
    public async Task ApiError_ThrowsIdeogramException()
    {
        // Arrange
        var errorResponse = new ErrorResponse
        {
            Error = "Invalid API key",
            Code = "INVALID_API_KEY"
        };
        
        var mockHandler = CreateMockHttpMessageHandler(HttpStatusCode.Unauthorized, errorResponse);
        using var httpClient = new HttpClient(mockHandler.Object);
        using var client = new IdeogramClient(TestApiKey, httpClient);
        
        var request = new GenerateImageRequest { Prompt = "Test" };
        
        // Act & Assert
        var exception = await Assert.ThrowsAsync<IdeogramException>(() => client.GenerateAsync(request));
        Assert.Equal(401, exception.StatusCode);
        Assert.Equal("INVALID_API_KEY", exception.ErrorCode);
    }
    
    private static Mock<HttpMessageHandler> CreateMockHttpMessageHandler<T>(HttpStatusCode statusCode, T responseObject)
    {
        var json = JsonSerializer.Serialize(responseObject, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        });
        
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            });
        
        return mockHandler;
    }
}
