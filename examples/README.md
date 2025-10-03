# Examples

This directory contains example code demonstrating how to use the Ideogram SDK.

## Running the Examples

1. Set your API key as an environment variable:
   ```bash
   export IDEOGRAM_API_KEY="your-api-key-here"
   ```

2. Each example demonstrates different functionality of the SDK.

## Example Scenarios

### Basic Image Generation

```csharp
using Ideogram.SDK;
using Ideogram.SDK.Models;

var apiKey = Environment.GetEnvironmentVariable("IDEOGRAM_API_KEY");
var client = new IdeogramClient(apiKey);

var request = new GenerateImageRequest
{
    Prompt = "A futuristic city skyline at night with neon lights",
    AspectRatio = AspectRatio.Aspect_16_9,
    Model = Model.V_2,
    StyleType = StyleType.Realistic
};

var response = await client.GenerateAsync(request);
Console.WriteLine($"Generated: {response.Data[0].Url}");
```

### Batch Generation

```csharp
var request = new GenerateImageRequest
{
    Prompt = "Beautiful landscape variations",
    NumImages = 4,
    AspectRatio = AspectRatio.Aspect_1_1
};

var response = await client.GenerateAsync(request);

foreach (var (image, index) in response.Data.Select((img, idx) => (img, idx)))
{
    Console.WriteLine($"Image {index + 1}: {image.Url}");
}
```

### Image Editing with Mask

```csharp
// Load your images
using var imageStream = File.OpenRead("original.png");
using var maskStream = File.OpenRead("edit-mask.png");

var editRequest = new EditImageRequest
{
    ImageFile = imageStream,
    MaskFile = maskStream,
    Prompt = "Replace the sky with a dramatic sunset",
    Model = Model.V_2
};

var result = await client.EditAsync(editRequest);
```

### Style Transfer with Remix

```csharp
using var imageStream = File.OpenRead("photo.jpg");

var remixRequest = new RemixImageRequest
{
    ImageFile = imageStream,
    Prompt = "Transform into a Van Gogh-style painting",
    ImageWeight = new ImageWeight { Weight = 0.8 }
};

var result = await client.RemixAsync(remixRequest);
```

### Image Analysis

```csharp
using var imageStream = File.OpenRead("mystery-image.jpg");

var describeRequest = new DescribeImageRequest
{
    ImageFile = imageStream
};

var result = await client.DescribeAsync(describeRequest);
Console.WriteLine($"AI Description: {result.Data[0].Description}");
```

### Error Handling

```csharp
try
{
    var response = await client.GenerateAsync(request);
}
catch (IdeogramException ex) when (ex.StatusCode == 401)
{
    Console.WriteLine("Invalid API key");
}
catch (IdeogramException ex) when (ex.StatusCode == 429)
{
    Console.WriteLine("Rate limit exceeded. Please wait before retrying.");
}
catch (IdeogramException ex)
{
    Console.WriteLine($"API Error: {ex.Message}");
}
```

### Using Custom Color Palettes

```csharp
var request = new GenerateImageRequest
{
    Prompt = "Abstract art composition",
    ColorPalette = new ColorPalette
    {
        Members = new List<ColorPaletteMember>
        {
            new() { Color = "#FF6B6B", Weight = 0.3 },
            new() { Color = "#4ECDC4", Weight = 0.3 },
            new() { Color = "#45B7D1", Weight = 0.4 }
        }
    }
};
```

### Reproducible Generation

```csharp
// Use the same seed to get consistent results
var seed = 42;

var request = new GenerateImageRequest
{
    Prompt = "A magical forest scene",
    Seed = seed,
    Model = Model.V_2
};

var response = await client.GenerateAsync(request);
// Will produce the same image when using the same seed and prompt
```

## Tips

- Always dispose of file streams after use
- Use cancellation tokens for long-running operations
- Check `IsImageSafe` flag in responses for content safety
- Store generated image URLs or download them promptly (they may expire)
- Use appropriate error handling for production applications
