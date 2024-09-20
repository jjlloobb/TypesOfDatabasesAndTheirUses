namespace CBTW.Microservices.UI.Domain.Models;

public class MongoDbDatabaseSettings
{
	public string ConnectionString { get; set; } = null!;

	public string DatabaseName { get; set; } = null!;

	public string LoggingCollectionName { get; set; } = null!;
}
