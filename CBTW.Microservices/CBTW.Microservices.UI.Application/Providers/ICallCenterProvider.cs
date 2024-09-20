using CBTW.Microservices.UI.Domain.CallCenter.ConsultarCiudades;
using CBTW.Microservices.UI.Domain.CallCenter.ConsultarClientes;
using CBTW.Microservices.UI.Domain.CallCenter.ConsultarTelefonosCC;
using CBTW.Microservices.UI.Domain.CallCenter.ConsultarTiposDeDocumento;
using CBTW.Microservices.UI.Domain.Models;

namespace CBTW.Microservices.UI.Application.Providers;

public interface ICallCenterProvider
{
    public Task<List<CustomerViewModel>> ConsultarClientes(CancellationToken cancellationToken);

    public Task<List<ConsultarClientesRespuestaValue>> ConsultarClientesRegistrados(CancellationToken cancellationToken);

    public Task<CustomerViewModel> ConsultarCliente(CustomerViewModel customerViewModel, CancellationToken cancellationToken);

    public Task CrearCliente(CustomerViewModel customerViewModel, CancellationToken cancellationToken);

    public Task ActualizarCliente(CustomerViewModel customerViewModel, CancellationToken cancellationToken);

    public Task<List<TiposDeDocumentoRespuestaValue>> ConsultarTiposDeDocumento(CancellationToken cancellationToken);

    public Task<List<ConsultarCiudadesRespuestaValue>> ConsultarCiudades(CancellationToken cancellationToken);

    public Task<List<ConsultarTelefonosCCRespuestaValue>> ConsultarTelefonosCC(CancellationToken cancellationToken);

    public Task<List<PQRViewModel>> ConsultarPQRs(CancellationToken cancellationToken);

    public Task<PQRViewModel> ConsultarPQR(PQRViewModel pqrViewModel, CancellationToken cancellationToken);

	public Task CrearPQR(PQRViewModel customerViewModel, CancellationToken cancellationToken);
}
