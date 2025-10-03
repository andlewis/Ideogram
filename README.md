# Ideogram .NET SDK

[![CI Build and Test](https://github.com/andlewis/Ideogram/actions/workflows/ci.yml/badge.svg)](https://github.com/andlewis/Ideogram/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/Ideogram.SDK.svg)](https://www.nuget.org/packages/Ideogram.SDK/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A comprehensive .NET SDK for the [Ideogram v3 REST API](https://developer.ideogram.ai/ideogram-api/api-overview), enabling AI-powered image generation, editing, remixing, and description capabilities in your .NET applications.

## Features

- 🎨 **Image Generation** - Generate images from text prompts with various styles and aspect ratios
- ✏️ **Image Editing** - Edit existing images with text prompts and optional masks
- 🔄 **Image Remixing** - Transform and remix existing images while maintaining certain characteristics
- 📝 **Image Description** - Generate detailed descriptions of images using AI
- 🔒 **Security First** - Built following OWASP guidelines with proper input validation and secure API key handling
- ⚡ **Async/Await** - Modern async API for optimal performance
- 📦 **Well-Typed** - Comprehensive models with full XML documentation
- 🧪 **Tested** - Thoroughly unit tested with high code coverage

## Installation

Install the package via NuGet Package Manager:

```bash
dotnet add package Ideogram.SDK
```

Or via Package Manager Console:

```powershell
Install-Package Ideogram.SDK
```

## Quick Start

### Initialize the Client

```csharp
using Ideogram.SDK;

var apiKey = "your-ideogram-api-key"; // Get from https://developer.ideogram.ai
var client = new IdeogramClient(apiKey);
```

### Generate an Image

```csharp
using Ideogram.SDK.Models;

var request = new GenerateImageRequest
{
    Prompt = "A serene mountain landscape at sunset with vibrant colors",
    AspectRatio = AspectRatio.Aspect_16_9,
    Model = Model.V_2,
    StyleType = StyleType.Realistic,
    NumImages = 1
};

var response = await client.GenerateAsync(request);

foreach (var image in response.Data)
{
    Console.WriteLine($"Generated image URL: {image.Url}");
    Console.WriteLine($"Prompt used: {image.Prompt}");
    Console.WriteLine($"Resolution: {image.Resolution}");
}
```

### Edit an Image

```csharp
using var imageStream = File.OpenRead("input.png");
var editRequest = new EditImageRequest
{
    ImageFile = imageStream,
    ImageFileName = "input.png",
    Prompt = "Add a beautiful rainbow in the sky",
    Model = Model.V_2
};

var editResponse = await client.EditAsync(editRequest);
```

### Remix an Image

```csharp
using var imageStream = File.OpenRead("input.png");
var remixRequest = new RemixImageRequest
{
    ImageFile = imageStream,
    ImageFileName = "input.png",
    Prompt = "Transform into a watercolor painting style",
    ImageWeight = new ImageWeight { Weight = 0.7 }
};

var remixResponse = await client.RemixAsync(remixRequest);
```

### Describe an Image

```csharp
using var imageStream = File.OpenRead("input.png");
var describeRequest = new DescribeImageRequest
{
    ImageFile = imageStream,
    ImageFileName = "input.png"
};

var describeResponse = await client.DescribeAsync(describeRequest);

foreach (var description in describeResponse.Data)
{
    Console.WriteLine($"Description: {description.Description}");
}
```

## Advanced Usage

### Custom Color Palettes

```csharp
var request = new GenerateImageRequest
{
    Prompt = "A modern abstract artwork",
    ColorPalette = new ColorPalette
    {
        Members = new List<ColorPaletteMember>
        {
            new ColorPaletteMember { Color = "#FF5733", Weight = 0.4 },
            new ColorPaletteMember { Color = "#33FF57", Weight = 0.3 },
            new ColorPaletteMember { Color = "#3357FF", Weight = 0.3 }
        }
    }
};
```

### Using Preset Color Palettes

```csharp
var request = new GenerateImageRequest
{
    Prompt = "A vibrant tropical scene",
    ColorPalette = new ColorPalette
    {
        Preset = ColorPalettePreset.Jungle
    }
};
```

### Magic Prompt Enhancement

```csharp
var request = new GenerateImageRequest
{
    Prompt = "sunset",
    MagicPromptOption = MagicPromptOption.On // Enhances the prompt automatically
};
```

### Reproducible Generation with Seeds

```csharp
var request = new GenerateImageRequest
{
    Prompt = "A unique artwork",
    Seed = 12345 // Use the same seed to get the same result
};
```

### Image Editing with Masks

```csharp
using var imageStream = File.OpenRead("input.png");
using var maskStream = File.OpenRead("mask.png");

var editRequest = new EditImageRequest
{
    ImageFile = imageStream,
    MaskFile = maskStream,
    MaskFileName = "mask.png",
    Prompt = "Replace masked area with a beautiful garden"
};
```

## Security Best Practices

This SDK implements OWASP security guidelines:

- **Secure API Key Handling** - API keys are transmitted securely via HTTPS Bearer authentication
- **Input Validation** - All user inputs are validated before being sent to the API
- **Error Handling** - Comprehensive error handling with typed exceptions
- **Stream Validation** - Image streams are validated before use
- **No Secrets in Code** - Use environment variables or secure configuration for API keys

```csharp
// Best practice: Use environment variables or configuration
var apiKey = Environment.GetEnvironmentVariable("IDEOGRAM_API_KEY");
var client = new IdeogramClient(apiKey);
```

## Dependency Injection

The SDK works great with dependency injection:

```csharp
// In Startup.cs or Program.cs
services.AddHttpClient<IIdeogramClient, IdeogramClient>()
    .ConfigureHttpClient(client => 
    {
        client.Timeout = TimeSpan.FromMinutes(5);
    });
```

## Error Handling

All API errors are wrapped in `IdeogramException`:

```csharp
try
{
    var response = await client.GenerateAsync(request);
}
catch (IdeogramException ex)
{
    Console.WriteLine($"API Error: {ex.Message}");
    Console.WriteLine($"Status Code: {ex.StatusCode}");
    Console.WriteLine($"Error Code: {ex.ErrorCode}");
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"Network Error: {ex.Message}");
}
```

## API Reference

### Models

- **AspectRatio** - Aspect ratios: 1:1, 16:9, 9:16, 3:2, 2:3, 4:3, 3:4, 10:16, 16:10, 1:3, 3:1
- **Model** - Available models: V_2, V_2_Turbo, V_1, V_1_Turbo
- **StyleType** - Style types: Auto, General, Realistic, Design, Render_3D, Anime
- **MagicPromptOption** - Magic prompt options: On, Off, Auto
- **ColorPalettePreset** - Preset palettes: Ember, Fresh, Jungle, Magic, Melon, Mosaic, Muted, Pastel, Realism, Rustic, Sakura

### Methods

- `GenerateAsync(GenerateImageRequest)` - Generate images from text
- `EditAsync(EditImageRequest)` - Edit existing images
- `RemixAsync(RemixImageRequest)` - Remix/transform images
- `DescribeAsync(DescribeImageRequest)` - Describe image content

## Requirements

- .NET 8.0 or later
- An Ideogram API key (get one at [https://developer.ideogram.ai](https://developer.ideogram.ai))

## Publishing to NuGet

This package uses GitHub Actions for automated publishing. To publish a new version:

1. Update the version in `src/Ideogram.SDK/Ideogram.SDK.csproj`
2. Create a git tag: `git tag v1.0.0`
3. Push the tag: `git push origin v1.0.0`
4. The GitHub Action will automatically build, test, and publish the package

Or use manual workflow dispatch from the GitHub Actions tab.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Support

For issues, questions, or contributions, please visit the [GitHub repository](https://github.com/andlewis/Ideogram).

For Ideogram API documentation, visit [https://developer.ideogram.ai](https://developer.ideogram.ai).

## Acknowledgments

This SDK is built for the [Ideogram API](https://ideogram.ai) - AI-powered image generation platform.
