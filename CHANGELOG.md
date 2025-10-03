# Changelog

All notable changes to the Ideogram .NET SDK will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2025-01-XX

### Added
- Initial release of Ideogram .NET SDK
- Support for image generation from text prompts
- Support for image editing with optional masks
- Support for image remixing/transformation
- Support for AI-powered image description
- Comprehensive model support (V1, V1 Turbo, V2, V2 Turbo)
- Multiple aspect ratio options
- Style type configuration (Auto, General, Realistic, Design, 3D Render, Anime)
- Color palette support (presets and custom colors)
- Magic prompt enhancement options
- Seed-based reproducible generation
- Full async/await support
- Comprehensive error handling with typed exceptions
- XML documentation for all public APIs
- Unit tests with high code coverage
- GitHub Actions workflow for CI/CD
- GitHub Actions workflow for automated NuGet publishing
- Security best practices following OWASP guidelines
- Comprehensive README with examples
- Contributing guidelines
- Security policy documentation

### Security
- Secure API key handling via Bearer authentication
- Input validation for all user inputs
- Stream validation for file uploads
- HTTPS enforcement for all API communication
- Protection against prompt injection
- Proper error handling without exposing sensitive information

[1.0.0]: https://github.com/andlewis/Ideogram/releases/tag/v1.0.0
