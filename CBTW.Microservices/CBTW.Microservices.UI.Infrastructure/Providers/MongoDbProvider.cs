using CBTW.Microservices.UI.Application.Providers;
using CBTW.Microservices.UI.Domain.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace CBTW.Microservices.UI.Infrastructure.Providers;

public class MongoDbProvider : IDocumentaryDbProvider
{
	private readonly IMongoCollection<CallCenterLog> callCenterLogCollection;

	public MongoDbProvider(IOptions<MongoDbDatabaseSettings> callCenterDatabaseSettings)
	{
		var mongoClient = new MongoClient(callCenterDatabaseSettings.Value.ConnectionString);

		var mongoDatabase = mongoClient.GetDatabase(callCenterDatabaseSettings.Value.DatabaseName);

		this.callCenterLogCollection = mongoDatabase.GetCollection<CallCenterLog>(callCenterDatabaseSettings.Value.LoggingCollectionName);

		var objectSerializer = new ObjectSerializer(type => ObjectSerializer.DefaultAllowedTypes(type) || type.FullName.StartsWith("CBTW.", StringComparison.InvariantCultureIgnoreCase));
		BsonSerializer.RegisterSerializer(objectSerializer);
	}

	public async Task<List<CallCenterLog>> GetCallCenterLogAsync() =>
		await this.callCenterLogCollection.Find(_ => true).ToListAsync();

	public async Task<CallCenterLog> GetCallCenterLogAsync(string id) =>
		await this.callCenterLogCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

	public async Task CreateCallCenterLogAsync(CallCenterLog newCallCenterLog) =>
		await this.callCenterLogCollection.InsertOneAsync(newCallCenterLog);

	public async Task UpdateCallCenterLogAsync(string id, CallCenterLog updatedCallCenterLog) =>
		await this.callCenterLogCollection.ReplaceOneAsync(x => x.Id == id, updatedCallCenterLog);

	public async Task RemoveCallCenterLogAsync(string id) =>
		await this.callCenterLogCollection.DeleteOneAsync(x => x.Id == id);
}