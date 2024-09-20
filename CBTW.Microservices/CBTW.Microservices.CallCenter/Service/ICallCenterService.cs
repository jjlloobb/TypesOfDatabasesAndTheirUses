using CBTW.Microservices.CallCenter.Service.Requests;
using CBTW.Microservices.CallCenter.Service.Responses;

namespace CBTW.Microservices.CallCenter.Service;

public interface ICallCenterService
{
    public Task<ConsultarClientesResponse> ConsultarClientes(CancellationToken cancellationToken);

    public Task<ConsultarClienteResponse> ConsultarCliente(ConsultarClienteRequest request, CancellationToken cancellationToken);

    public Task<CrearClienteResponse> CrearCliente(CrearClienteRequest request, CancellationToken cancellationToken);

    public Task<ActualizarClienteResponse> ActualizarCliente(ActualizarClienteRequest request, CancellationToken cancellationToken);

    public Task<ConsultarTiposDeDocumentoResponse> ConsultarTiposDeDocumento(CancellationToken cancellationToken);

    public Task<ConsultarCiudadesResponse> ConsultarCiudades(CancellationToken cancellationToken);

    public Task<ConsultarTelefonosCCResponse> ConsultarTelefonosCC(CancellationToken cancellationToken);

    public Task<ConsultarPQRsResponse> ConsultarPQRs(CancellationToken cancellationToken);

    public Task<ConsultarPQRResponse> ConsultarPQR(ConsultarPQRRequest request, CancellationToken cancellationToken);

    public Task<CrearPQRResponse> CrearPQR(CrearPQRRequest request, CancellationToken cancellationToken);
}
