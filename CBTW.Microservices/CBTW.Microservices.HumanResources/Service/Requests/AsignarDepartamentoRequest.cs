using CBTW.Microservices.HumanResources.Service.Responses;
using MediatR;

namespace CBTW.Microservices.HumanResources.Service.Requests;

public class AsignarDepartamentoRequest : IRequest<AsignarDepartamentoResponse>
{
	public string TipoDocumento { get; set; }

	public string Documento { get; set; }

	public string Abreviacion { get; set; }
}