using CBTW.Microservices.HumanResources.Service;
using CBTW.Microservices.HumanResources.Service.Requests;
using CBTW.Microservices.HumanResources.Service.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CBTW.Microservices.HumanResources.Infrastructure.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class HumanResourcesController : ControllerBase, IHumanResourcesService
{
	private readonly IMediator mediator;

	public HumanResourcesController(IMediator mediator)
	 => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

	[HttpGet]
	public string Index()
		=> $"Response from HumanResources Service at {DateTime.Now}";

	[HttpGet]
	public Task<ConsultarEmpleadosResponse> ConsultarEmpleados(CancellationToken cancellationToken)
		=> this.mediator.Send(new ConsultarEmpleadosRequest(), cancellationToken);

	[HttpPost]
	public Task<ConsultarEmpleadoResponse> ConsultarEmpleado([FromBody] ConsultarEmpleadoRequest request, CancellationToken cancellationToken)
		=> this.mediator.Send(request, cancellationToken);

	[HttpPost]
	public Task<CrearEmpleadoResponse> CrearEmpleado([FromBody] CrearEmpleadoRequest request, CancellationToken cancellationToken)
		=> this.mediator.Send(request, cancellationToken);

	[HttpPut]
	public Task<ActualizarEmpleadoResponse> ActualizarEmpleado([FromBody] ActualizarEmpleadoRequest request, CancellationToken cancellationToken)
		=> this.mediator.Send(request, cancellationToken);

	[HttpGet]
	public Task<ConsultarDepartamentosResponse> ConsultarDepartamentos(CancellationToken cancellationToken)
		=> this.mediator.Send(new ConsultarDepartamentosRequest(), cancellationToken);

	[HttpPost]
	public Task<ConsultarDepartamentoResponse> ConsultarDepartamento([FromBody] ConsultarDepartamentoRequest request, CancellationToken cancellationToken)
		=> this.mediator.Send(request, cancellationToken);

	[HttpPost]
	public Task<CrearDepartamentoResponse> CrearDepartamento([FromBody] CrearDepartamentoRequest request, CancellationToken cancellationToken)
		=> this.mediator.Send(request, cancellationToken);

	[HttpPut]
	public Task<ActualizarDepartamentoResponse> ActualizarDepartamento([FromBody] ActualizarDepartamentoRequest request, CancellationToken cancellationToken)
		=> this.mediator.Send(request, cancellationToken);

	[HttpPost]
	public Task<AsignarDepartamentoResponse> AsignarDepartamento([FromBody] AsignarDepartamentoRequest request, CancellationToken cancellationToken)
		=> this.mediator.Send(request, cancellationToken);
}