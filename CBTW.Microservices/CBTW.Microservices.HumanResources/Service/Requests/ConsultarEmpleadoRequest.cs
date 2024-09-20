using CBTW.Microservices.HumanResources.Service.Responses;
using MediatR;

namespace CBTW.Microservices.HumanResources.Service.Requests;

public class ConsultarEmpleadoRequest : IRequest<ConsultarEmpleadoResponse>
{
    public string TipoDocumento { get; set; }

    public string Documento { get; set; }
}
