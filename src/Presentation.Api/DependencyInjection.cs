using System.Reflection;
using Microsoft.OpenApi.Models;

namespace Presentation.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(
            options =>
            {
                var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                var filePath = Path.Combine(
                    path1: AppContext.BaseDirectory,
                    path2: assemblyName + ".xml"
                );
                options.IncludeXmlComments(filePath);
            }
        );

        return services;
    }
}
