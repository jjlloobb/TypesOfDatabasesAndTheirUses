using CBTW.Microservicios.CallCenter.Aplicacion.Configurations;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class CallCenterApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddCallCenterApplication(this IServiceCollection services)
	{
		var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

		services.AddOptions();

		services.Configure<RedisOptions>(configuration.GetSection("ServicesConfiguration:DistributedCache:Redis"));

		services.AddLogging();

        return services;
    }
}
