using CBTW.Microservices.CallCenter.Service.Responses;
using MediatR;

namespace CBTW.Microservices.CallCenter.Service.Requests;

public class CrearPQRRequest : IRequest<CrearPQRResponse>
{
    public string Customer { get; set; }

    public string Asunto { get; set; }

    public string Descripcion { get; set; }
}
