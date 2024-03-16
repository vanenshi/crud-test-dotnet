using Microsoft.AspNetCore.Authentication;

namespace Presentation.Api;

public static class RequestPipeline
{
    public static IApplicationBuilder UsePresentation(
        this IApplicationBuilder app,
        IConfiguration configuration
    )
    {
        return app;
    }
}
