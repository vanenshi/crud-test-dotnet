using System.Net;

namespace Application.Exceptions;

public class NotFoundException : ApiException
{
    public NotFoundException(string? message)
        : base(statusCode: HttpStatusCode.NotFound, message: message) { }

    public NotFoundException(string name, object key)
        : base(
            statusCode: HttpStatusCode.NotFound,
            message: $"Object '{name}' ({key}) was not found!".ToString()
        ) { }
}
