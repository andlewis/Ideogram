# Security Policy

## Supported Versions

We release patches for security vulnerabilities in the following versions:

| Version | Supported          |
| ------- | ------------------ |
| 1.x.x   | :white_check_mark: |

## Reporting a Vulnerability

We take security seriously. If you discover a security vulnerability, please follow these steps:

1. **Do NOT** open a public issue
2. Email the maintainer directly with details about the vulnerability
3. Include steps to reproduce the vulnerability if possible
4. Allow reasonable time for the issue to be addressed before public disclosure

## Security Best Practices

When using this SDK, please follow these security best practices:

### API Key Management

- **Never** commit API keys to source control
- Store API keys in environment variables or secure configuration stores
- Use key rotation policies for production environments
- Limit API key permissions to only what's needed

```csharp
// Good - Using environment variables
var apiKey = Environment.GetEnvironmentVariable("IDEOGRAM_API_KEY");
var client = new IdeogramClient(apiKey);

// Bad - Hardcoding API keys
var client = new IdeogramClient("sk-1234567890abcdef"); // ❌ Never do this
```

### Input Validation

The SDK performs input validation, but you should also:

- Validate user inputs before passing to the SDK
- Sanitize file uploads before processing
- Implement rate limiting for API calls in your application
- Monitor for unusual patterns or excessive API usage

### Network Security

- Always use HTTPS (enforced by the SDK)
- Implement proper timeout handling
- Use cancellation tokens for long-running operations
- Consider implementing retry policies with exponential backoff

### Dependency Security

- Keep the SDK and its dependencies up to date
- Regularly check for security advisories
- Use `dotnet list package --vulnerable` to check for vulnerable dependencies

## OWASP Compliance

This SDK follows OWASP guidelines including:

- **A01:2021 - Broken Access Control** - Secure API key handling
- **A02:2021 - Cryptographic Failures** - All communication over HTTPS
- **A03:2021 - Injection** - Input validation and sanitization
- **A04:2021 - Insecure Design** - Secure defaults, proper error handling
- **A05:2021 - Security Misconfiguration** - Secure configuration practices
- **A07:2021 - Identification and Authentication Failures** - Secure authentication
- **A08:2021 - Software and Data Integrity Failures** - Signed packages
- **A09:2021 - Security Logging and Monitoring Failures** - Comprehensive error reporting

## Known Security Considerations

### Rate Limiting

The Ideogram API has rate limits. Your application should:
- Implement proper rate limiting
- Handle 429 (Too Many Requests) responses gracefully
- Use exponential backoff for retries

### Content Safety

- The SDK returns `IsImageSafe` flag in responses
- Always check this flag in production applications
- Implement content moderation policies appropriate for your use case

### Stream Handling

When working with image streams:
- Validate file types and sizes before upload
- Implement virus scanning for user-uploaded content
- Dispose of streams properly to prevent memory leaks

## Updates and Patches

We aim to:
- Address critical security issues within 48 hours
- Release patches for high-severity issues within 7 days
- Include security fixes in regular releases for lower severity issues

## Security Checklist for Users

- [ ] API keys stored securely (environment variables, Azure Key Vault, etc.)
- [ ] No secrets in source code or configuration files
- [ ] Input validation implemented in your application
- [ ] Rate limiting configured
- [ ] Error handling doesn't expose sensitive information
- [ ] Dependencies are up to date
- [ ] HTTPS enforced for all communications
- [ ] Proper logging and monitoring in place
- [ ] Regular security audits performed

## Contact

For security concerns, please create a security advisory in the GitHub repository.
