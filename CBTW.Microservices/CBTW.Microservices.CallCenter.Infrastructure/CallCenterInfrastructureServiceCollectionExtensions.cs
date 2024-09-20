using AutoMapper;
using CBTW.Microservices.CallCenter.Application;
using CBTW.Microservices.CallCenter.Application.Providers;
using CBTW.Microservices.CallCenter.Infrastructure.Controllers;
using CBTW.Microservices.CallCenter.Infrastructure.Providers;
using CBTW.Microservices.CallCenter.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class CallCenterInfrastructureServiceCollectionExtensions
{
	public static IServiceCollection AddCallCenterInfrastucture(this IServiceCollection services)
	{
		var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

		//services.AddDbContext<SqlServerCallCenterDbContext>(options =>
		//{
		//	options.UseSqlServer(configuration.GetConnectionString("SqlServerDatabaseConnection"));
		//},
		//ServiceLifetime.Transient,
		//ServiceLifetime.Transient);
		//services.AddTransient<IUnitOfWork, SqlServerCallCenterUnitOfWork>();

		services.AddDbContext<MySqlCallCenterDbContext>(options =>
		{
			options.UseMySql(configuration.GetConnectionString("MySqlDatabaseConnection"), ServerVersion.AutoDetect(configuration.GetConnectionString("MySqlDatabaseConnection")));
		},
		ServiceLifetime.Transient,
		ServiceLifetime.Transient);
		services.AddTransient<IUnitOfWork, MySqlCallCenterUnitOfWork>();

		//services.AddDbContext<PostgreSqlCallCenterDbContext>(options =>
		//{
		//	options.UseNpgsql(configuration.GetConnectionString("PostgreSqlDatabaseConnection"));
		//},
		//ServiceLifetime.Transient,
		//ServiceLifetime.Transient);
		//services.AddTransient<IUnitOfWork, PostgreSqlCallCenterUnitOfWork>();

		//services.AddDbContext<MariaDbCallCenterDbContext>(options =>
		//{
		//	options.UseMySql(configuration.GetConnectionString("MariaDbDatabaseConnection"), ServerVersion.AutoDetect(configuration.GetConnectionString("MariaDbDatabaseConnection")));
		//},
		//ServiceLifetime.Transient,
		//ServiceLifetime.Transient);
		//services.AddTransient<IUnitOfWork, MariaDbCallCenterUnitOfWork>();

		//services.AddDbContext<OracleCallCenterDbContext>(options =>
		//{
		//	options.UseOracle(configuration.GetConnectionString("OracleDatabaseConnection"), b =>
		//		b.UseOracleSQLCompatibility("11"));
		//},
		//ServiceLifetime.Transient,
		//ServiceLifetime.Transient);
		//services.AddTransient<IUnitOfWork, OracleCallCenterUnitOfWork>();

		services.AddStackExchangeRedisCache(options =>
		{
			options.Configuration = configuration.GetConnectionString("RedisDatabaseConnection");
		});
		services.AddTransient<IMemoryProvider, RedisProvider>();		

		services.AddTransient<ICallCenterService, CallCenterController>();

		services.AddSingleton(provider => new MapperConfiguration(cfg =>
		{
			cfg.AddProfile(new ApplicationMappings());
		}).CreateMapper());

		return services;
	}
}