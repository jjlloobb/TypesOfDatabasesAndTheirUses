using CBTW.Microservices.UI.Domain.Models;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class UIApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddUIApplication(
        this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

        services.AddOptions();

		services.Configure<MongoDbDatabaseSettings>(configuration.GetSection("MongoDbDatabase"));

		services.AddLogging();

        var applicationInsights = configuration["ApplicationInsights:InstrumentationKey"];
        if (!string.IsNullOrEmpty(applicationInsights))
        {
            services.AddApplicationInsightsTelemetry(applicationInsights);
            services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, o) => { module.EnableSqlCommandTextInstrumentation = true; });
        }

        return services;
    }
}