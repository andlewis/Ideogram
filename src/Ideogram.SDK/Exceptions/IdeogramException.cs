namespace Ideogram.SDK.Exceptions;

/// <summary>
/// Exception thrown when an API request fails
/// </summary>
public class IdeogramException : Exception
{
    /// <summary>
    /// HTTP status code from the failed request
    /// </summary>
    public int? StatusCode { get; }
    
    /// <summary>
    /// Error code from the API response
    /// </summary>
    public string? ErrorCode { get; }
    
    /// <summary>
    /// Initializes a new instance of IdeogramException
    /// </summary>
    public IdeogramException(string message) : base(message)
    {
    }
    
    /// <summary>
    /// Initializes a new instance of IdeogramException with status code
    /// </summary>
    public IdeogramException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
    
    /// <summary>
    /// Initializes a new instance of IdeogramException with status code and error code
    /// </summary>
    public IdeogramException(string message, int statusCode, string? errorCode) : base(message)
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
    }
    
    /// <summary>
    /// Initializes a new instance of IdeogramException with inner exception
    /// </summary>
    public IdeogramException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
