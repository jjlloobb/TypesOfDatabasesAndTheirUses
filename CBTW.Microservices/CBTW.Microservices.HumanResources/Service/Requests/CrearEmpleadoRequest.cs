using CBTW.Microservices.HumanResources.Service.Responses;
using MediatR;

namespace CBTW.Microservices.HumanResources.Service.Requests;

public class CrearEmpleadoRequest : IRequest<CrearEmpleadoResponse>
{
    public string TipoDocumento { get; set; }

    public string Documento { get; set; }

    public string NombreCompleto { get; set; }

    public string CodigoPaisCelular { get; set; }

    public string Celular { get; set; }

    public DateTime FechaNacimiento { get; set; }
}
