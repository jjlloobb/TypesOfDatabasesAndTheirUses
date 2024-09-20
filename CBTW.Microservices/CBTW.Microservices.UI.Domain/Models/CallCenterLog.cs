using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CBTW.Microservices.UI.Domain.Models;

public class CallCenterLog
{
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string Id { get; set; }

	public string TypeLog { get; set; }

	public string LogLevel { get; set; }

    public string MachineName { get; set; }

    public string ProjectName { get; set; }

    public string ClassName { get; set; }

	public string Method { get; set; }
	
	public dynamic Body { get; set; }

	public DateTime DateCreated { get; set; }
}