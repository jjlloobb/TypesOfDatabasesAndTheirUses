using CBTW.Microservices.HumanResources.Service.Responses;
using MediatR;

namespace CBTW.Microservices.HumanResources.Service.Requests;

public class CrearDepartamentoRequest : IRequest<CrearDepartamentoResponse>
{
    public string Abreviacion { get; set; }

    public string NombreCompleto { get; set; }

	public string Descripcion { get; set; }

	public string CodigoPaisCelular { get; set; }

	public string Celular { get; set; }
}