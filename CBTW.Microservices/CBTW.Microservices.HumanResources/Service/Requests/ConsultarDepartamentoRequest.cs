using CBTW.Microservices.HumanResources.Service.Responses;
using MediatR;

namespace CBTW.Microservices.HumanResources.Service.Requests;

public class ConsultarDepartamentoRequest : IRequest<ConsultarDepartamentoResponse>
{
    public string Abreviacion { get; set; }
}
