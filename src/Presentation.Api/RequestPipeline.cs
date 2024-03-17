using Presentation.Api.Middleware;

namespace Presentation.Api;

public static class RequestPipeline
{
    public static IApplicationBuilder UsePresentation(
        this IApplicationBuilder app,
        IConfiguration configuration
    )
    {
        app.UseMiddleware<ErrorHandlingMiddleware>();
        return app;
    }
}
