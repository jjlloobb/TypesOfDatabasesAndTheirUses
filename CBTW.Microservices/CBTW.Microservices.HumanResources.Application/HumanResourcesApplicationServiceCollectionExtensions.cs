using CBTW.Microservicios.HumanResources.Aplicacion.Configurations;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class HumanResourcesApplicationServiceCollectionExtensions
{
	public static IServiceCollection AddHumanResourcesApplication(this IServiceCollection services)
	{
		var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

		services.AddOptions();

		services.Configure<Neo4jDatabaseSettings>(configuration.GetSection("ServicesConfiguration:Neo4j"));

		services.AddLogging();

		return services;
	}
}