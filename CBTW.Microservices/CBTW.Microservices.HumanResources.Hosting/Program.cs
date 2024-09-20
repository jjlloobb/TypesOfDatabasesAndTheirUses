using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CBTW.Microservices.HumanResources.Hosting;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Host.ConfigurePlatformArchitecture();

		// Add services to the container.

		builder.Services.AddHumanResourcesApplication();

		builder.Services.AddHumanResourcesInfrastucture();

		builder.Services.AddControllers();

		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo { Title = "HumanResources Microservice API", Version = "v1" });
		});

		var app = builder.Build();

		app.UsePlatformArchitecture();

		app.UseHttpsRedirection();

		app.UseRouting();

		app.UseAuthorization();

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers();
		});

		app.UseSwagger(c =>
		{
			c.SerializeAsV2 = true;
		});

		app.UseSwaggerUI(c =>
		{
			c.SwaggerEndpoint("/swagger/v1/swagger.json", "HumanResources Microservice API V1");
		});

		app.Run();
	}
}