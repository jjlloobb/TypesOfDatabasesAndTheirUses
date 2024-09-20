using CBTW.Microservices.HumanResources.Service.Requests;
using CBTW.Microservices.HumanResources.Service.Responses;

namespace CBTW.Microservices.HumanResources.Service;

public interface IHumanResourcesService
{
    public Task<ConsultarEmpleadosResponse> ConsultarEmpleados(CancellationToken cancellationToken);

    public Task<ConsultarEmpleadoResponse> ConsultarEmpleado(ConsultarEmpleadoRequest request, CancellationToken cancellationToken);

    public Task<CrearEmpleadoResponse> CrearEmpleado(CrearEmpleadoRequest request, CancellationToken cancellationToken);

    public Task<ActualizarEmpleadoResponse> ActualizarEmpleado(ActualizarEmpleadoRequest request, CancellationToken cancellationToken);

    public Task<ConsultarDepartamentosResponse> ConsultarDepartamentos(CancellationToken cancellationToken);

    public Task<ConsultarDepartamentoResponse> ConsultarDepartamento(ConsultarDepartamentoRequest request, CancellationToken cancellationToken);

    public Task<CrearDepartamentoResponse> CrearDepartamento(CrearDepartamentoRequest request, CancellationToken cancellationToken);

	public Task<ActualizarDepartamentoResponse> ActualizarDepartamento(ActualizarDepartamentoRequest request, CancellationToken cancellationToken);

	public Task<AsignarDepartamentoResponse> AsignarDepartamento(AsignarDepartamentoRequest request, CancellationToken cancellationToken);
}