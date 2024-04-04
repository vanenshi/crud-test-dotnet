using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly bool _includeErrorDetail;

    public ErrorHandlingMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _includeErrorDetail = configuration.GetSection("IncludeErrorDetail").Get<bool>();
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ApiException ex)
        {
            await HandleExceptionAsync(context: httpContext, exception: ex);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context: httpContext, exception: ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, ApiException exception)
    {
        var problemDetails = new ProblemDetails
        {
            Status = (int)exception.StatusCode,
            Title = exception.GetType().Name,
            Detail = exception.Message
        };

        if (_includeErrorDetail)
        {
            problemDetails.Extensions["StackTrace"] = exception.StackTrace;
        }

        return WriteApiResult(context: context, result: problemDetails);
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var problemDetails = new ProblemDetails
        {
            Status = (int)HttpStatusCode.InternalServerError,
            Title = "InternalServerError",
            Detail = null,
        };

        if (_includeErrorDetail)
        {
            problemDetails.Detail = exception.Message;
            problemDetails.Extensions["StackTrace"] = exception.StackTrace;
        }

        return WriteApiResult(context: context, result: problemDetails);
    }

    private Task WriteApiResult(HttpContext context, ProblemDetails result)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = result.Status ?? (int)HttpStatusCode.InternalServerError;

        var serializedResult = JsonSerializer.Serialize(value: result, options: options);
        return context.Response.WriteAsync(serializedResult);
    }
}
