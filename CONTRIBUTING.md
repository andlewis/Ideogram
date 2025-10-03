# Contributing to Ideogram .NET SDK

Thank you for your interest in contributing to the Ideogram .NET SDK! We welcome contributions from the community.

## Code of Conduct

Please be respectful and constructive in all interactions.

## How to Contribute

### Reporting Bugs

If you find a bug, please open an issue with:
- A clear, descriptive title
- Steps to reproduce the issue
- Expected vs actual behavior
- Environment details (.NET version, OS, etc.)
- Code samples if applicable

### Suggesting Features

Feature requests are welcome! Please open an issue with:
- A clear description of the feature
- Use cases and benefits
- Any implementation ideas you have

### Pull Requests

1. **Fork the repository** and create a new branch from `main`
2. **Make your changes** following the coding standards below
3. **Add tests** for any new functionality
4. **Update documentation** if needed
5. **Ensure all tests pass** by running `dotnet test`
6. **Submit a pull request** with a clear description of changes

## Development Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/andlewis/Ideogram.git
   cd Ideogram
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the solution:
   ```bash
   dotnet build
   ```

4. Run tests:
   ```bash
   dotnet test
   ```

## Coding Standards

- Follow the existing code style (see `.editorconfig`)
- Write XML documentation comments for public APIs
- Keep methods focused and single-purpose
- Use meaningful variable and method names
- Follow SOLID principles

### C# Style Guidelines

- Use PascalCase for class names, method names, and properties
- Use camelCase for local variables and parameters
- Prefix interface names with `I`
- Use `var` when the type is obvious
- Use nullable reference types where appropriate
- Add XML documentation comments for all public members

Example:
```csharp
/// <summary>
/// Generates an image based on the provided request
/// </summary>
/// <param name="request">The image generation request parameters</param>
/// <param name="cancellationToken">Cancellation token</param>
/// <returns>The generated image response</returns>
public async Task<ImageResponse> GenerateAsync(
    GenerateImageRequest request, 
    CancellationToken cancellationToken = default)
{
    // Implementation
}
```

## Testing

- Write unit tests for all new functionality
- Aim for high code coverage
- Use meaningful test names that describe what is being tested
- Follow the Arrange-Act-Assert pattern

Example test:
```csharp
[Fact]
public async Task GenerateAsync_WithValidRequest_ReturnsResponse()
{
    // Arrange
    var client = new IdeogramClient(TestApiKey);
    var request = new GenerateImageRequest { Prompt = "Test" };
    
    // Act
    var result = await client.GenerateAsync(request);
    
    // Assert
    Assert.NotNull(result);
}
```

## Documentation

- Update README.md for user-facing changes
- Add XML documentation comments for public APIs
- Include code examples for new features
- Update CHANGELOG.md with your changes

## Commit Messages

Write clear, concise commit messages:
- Use present tense ("Add feature" not "Added feature")
- Keep the first line under 72 characters
- Reference issues and PRs when applicable

Example:
```
Add support for custom timeout configuration

- Add Timeout property to IdeogramClient
- Update tests for timeout handling
- Update documentation with timeout examples

Fixes #123
```

## Pull Request Process

1. Ensure all tests pass
2. Update documentation as needed
3. Add entry to CHANGELOG.md
4. Request review from maintainers
5. Address any review feedback
6. Once approved, a maintainer will merge your PR

## Release Process

Releases are managed by maintainers:
1. Update version in `.csproj` file
2. Update CHANGELOG.md
3. Create a git tag (e.g., `v1.0.0`)
4. Push the tag to trigger automated release

## Questions?

Feel free to open an issue for any questions or concerns.

## License

By contributing, you agree that your contributions will be licensed under the MIT License.
