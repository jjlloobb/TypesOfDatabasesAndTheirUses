using AutoMapper;
using CBTW.Microservices.UI.Application.Providers;
using CBTW.Microservices.UI.Infrastructure;
using CBTW.Microservices.UI.Infrastructure.Providers;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class UIInfrastructureServiceCollectionExtensions
{
	public static IServiceCollection AddUIInfrastructure(
		this IServiceCollection services)
	{
		var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

		services.AddSingleton<IDocumentaryDbProvider, MongoDbProvider>();

		services.AddTransient<ICallCenterProvider, CallCenterApiProvider>();

		services.AddHttpClient();
		services.AddHttpClient("CallCenterProvider", (sp, client) =>
		{
			client.BaseAddress = new Uri(configuration.GetConnectionString("CallCenterServiceConnection"));
		});

		services.AddSingleton(provider => new MapperConfiguration(cfg =>
		{
			cfg.AddProfile(new InfrastructureMappings());
		}).CreateMapper());

		return services;
	}
}