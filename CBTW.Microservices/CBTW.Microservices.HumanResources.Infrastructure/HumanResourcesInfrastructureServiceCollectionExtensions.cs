using AutoMapper;
using CBTW.Microservices.HumanResources.Application;
using CBTW.Microservices.HumanResources.Infrastructure.Controllers;
using CBTW.Microservices.HumanResources.Service;
using CBTW.Microservicios.HumanResources.Aplicacion.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Neo4jClient;

namespace Microsoft.Extensions.DependencyInjection;

public static class HumanResourcesInfrastructureServiceCollectionExtensions
{
	public static IServiceCollection AddHumanResourcesInfrastucture(this IServiceCollection services)
	{
		var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

		var options = services.BuildServiceProvider().GetRequiredService<IOptions<Neo4jDatabaseSettings>>();
		var neo4jClient = new BoltGraphClient(options.Value.Connection, options.Value.User, options.Value.Password);
		neo4jClient.ConnectAsync();
		services.AddSingleton<IGraphClient>(neo4jClient);

		services.AddTransient<IHumanResourcesService, HumanResourcesController>();

		services.AddSingleton(provider => new MapperConfiguration(cfg =>
		{
			cfg.AddProfile(new ApplicationMappings());
		}).CreateMapper());

		return services;
	}
}