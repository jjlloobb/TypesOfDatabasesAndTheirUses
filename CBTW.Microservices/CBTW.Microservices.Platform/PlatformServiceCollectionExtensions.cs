using CBTW.Microservices.Platform.ExceptionHandling;
using FluentValidation;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class PlatformServiceCollectionExtensions
{
    public static IHostBuilder ConfigurePlatformArchitecture(
        this IHostBuilder host)
    {
        host.ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
        {
            var hostingEnvironment = hostBuilderContext.HostingEnvironment;

            var lConfigurationBuilder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            configurationBuilder.AddEnvironmentVariables();
        })
        .ConfigureServices((hostBuilderContext, services) =>
        {
            IConfiguration configuration = hostBuilderContext.Configuration;
            IHostEnvironment env = hostBuilderContext.HostingEnvironment;

            services.AddCors(confg =>
                    confg.AddPolicy("AllowAll",
                        p => p.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()));

            services.AddHttpContextAccessor();

            services.AddLogging();

            var applicationInsights = configuration["ApplicationInsights:InstrumentationKey"];
            if (!string.IsNullOrEmpty(applicationInsights))
            {
                services.AddApplicationInsightsTelemetry(applicationInsights);
                services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, o) => { module.EnableSqlCommandTextInstrumentation = true; });
            }

            void ScanAssembly(List<Assembly> assemblies, AssemblyName assemblyName)
            {
                if (!assemblyName.Name.StartsWith("CBTW.", StringComparison.InvariantCultureIgnoreCase))
                    return;

                if (assemblies.Where(j => string.Compare(j.GetName().FullName, assemblyName.FullName, StringComparison.InvariantCultureIgnoreCase) == 0).Any())
                    return;

                var loadedAssembly = Assembly.Load(assemblyName);
                assemblies.Add(loadedAssembly);

                foreach (var a in loadedAssembly.GetReferencedAssemblies())
                    ScanAssembly(assemblies, a);
            }

            var assemblies = new List<Assembly>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                ScanAssembly(assemblies, assembly.GetName());

            var assemblyArray = assemblies.ToArray();
            
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblyArray));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));

            services.AddFluentValidation(assemblyArray);
        });

        return host;
    }

    public static IApplicationBuilder UsePlatformArchitecture(
        this IApplicationBuilder app)
    {
        app.UsePlatformExceptionHandling();

        return app;
    }
}