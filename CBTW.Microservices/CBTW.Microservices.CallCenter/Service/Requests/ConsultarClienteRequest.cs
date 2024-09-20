using CBTW.Microservices.CallCenter.Service.Responses;
using MediatR;

namespace CBTW.Microservices.CallCenter.Service.Requests;

public class ConsultarClienteRequest : IRequest<ConsultarClienteResponse>
{
    public long? Id { get; set; }

    public int? TipoDocumento { get; set; }

    public string Documento { get; set; }
}
