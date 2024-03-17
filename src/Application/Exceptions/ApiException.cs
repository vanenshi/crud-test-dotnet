using System.Net;

namespace Application.Exceptions;

public abstract class ApiException : Exception
{
    public HttpStatusCode StatusCode { get; }

    protected ApiException(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }

    protected ApiException(HttpStatusCode statusCode, string? message) : base(message)
    {
        StatusCode = statusCode;
    }
}
