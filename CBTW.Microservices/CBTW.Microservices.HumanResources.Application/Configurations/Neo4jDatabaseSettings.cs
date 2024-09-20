namespace CBTW.Microservicios.HumanResources.Aplicacion.Configurations;

/// <summary>
/// Clase options neo4j
/// </summary>
public class Neo4jDatabaseSettings
{
	public Uri Connection { get; set; }

	public string User { get; set; }

	public string Password { get; set; }

	public string Database { get; set; }
}
