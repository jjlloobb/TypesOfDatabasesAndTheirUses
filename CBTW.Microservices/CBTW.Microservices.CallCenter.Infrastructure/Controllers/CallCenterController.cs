using CBTW.Microservices.CallCenter.Service;
using CBTW.Microservices.CallCenter.Service.Requests;
using CBTW.Microservices.CallCenter.Service.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CBTW.Microservices.CallCenter.Infrastructure.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CallCenterController : ControllerBase, ICallCenterService
{
    private readonly IMediator mediator;

    public CallCenterController(IMediator mediator)
     => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet]
    public string Index()
        => $"Response from CallCenter Service at {DateTime.Now}";

    [HttpGet]
    public Task<ConsultarClientesResponse> ConsultarClientes(CancellationToken cancellationToken)
        => this.mediator.Send(new ConsultarClientesRequest(), cancellationToken);

    [HttpPost]
    public Task<ConsultarClienteResponse> ConsultarCliente([FromBody] ConsultarClienteRequest request, CancellationToken cancellationToken)
        => this.mediator.Send(request, cancellationToken);

    [HttpPost]
    public Task<CrearClienteResponse> CrearCliente([FromBody] CrearClienteRequest request, CancellationToken cancellationToken)
        => this.mediator.Send(request, cancellationToken);

    [HttpPut]
    public Task<ActualizarClienteResponse> ActualizarCliente([FromBody] ActualizarClienteRequest request, CancellationToken cancellationToken)
        => this.mediator.Send(request, cancellationToken);

    [HttpGet]
    public Task<ConsultarTiposDeDocumentoResponse> ConsultarTiposDeDocumento(CancellationToken cancellationToken)
        => this.mediator.Send(new ConsultarTiposDeDocumentoRequest(), cancellationToken);

    [HttpGet]
    public Task<ConsultarCiudadesResponse> ConsultarCiudades(CancellationToken cancellationToken)
        => this.mediator.Send(new ConsultarCiudadesRequest(), cancellationToken);

    [HttpGet]
    public Task<ConsultarTelefonosCCResponse> ConsultarTelefonosCC(CancellationToken cancellationToken)
        => this.mediator.Send(new ConsultarTelefonosCCRequest(), cancellationToken);

    [HttpGet]
    public Task<ConsultarPQRsResponse> ConsultarPQRs(CancellationToken cancellationToken)
        => this.mediator.Send(new ConsultarPQRsRequest(), cancellationToken);

	[HttpGet]
    public Task<ConsultarPQRResponse> ConsultarPQR([FromBody] ConsultarPQRRequest request, CancellationToken cancellationToken)
        => this.mediator.Send(request, cancellationToken);

    [HttpPost]
    public Task<CrearPQRResponse> CrearPQR([FromBody] CrearPQRRequest request, CancellationToken cancellationToken)
        => this.mediator.Send(request, cancellationToken);
}
