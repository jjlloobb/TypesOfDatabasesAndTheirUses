using CBTW.Microservices.CallCenter.Service.Responses;
using MediatR;

namespace CBTW.Microservices.CallCenter.Service.Requests;

public class ConsultarPQRRequest : IRequest<ConsultarPQRResponse>
{
    public long? Id { get; set; }

    public long? IdCustomer { get; set; }
}
