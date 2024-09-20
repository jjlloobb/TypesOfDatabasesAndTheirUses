namespace CBTW.Microservices.HumanResources.Service.Responses;

public class EmpleadoResponse
{
	public string TipoDocumento { get; set; }

	public string Documento { get; set; }

	public string NombreCompleto { get; set; }

	public string CodigoPaisCelular { get; set; }

	public string Celular { get; set; }

	public DateTime FechaNacimiento { get; set; }
}
