using AutoMapper;
using CBTW.Microservices.HumanResources.Domain.HumanResources;
using CBTW.Microservices.HumanResources.Service.Requests;
using CBTW.Microservices.HumanResources.Service.Responses;
using MediatR;
using Neo4jClient;

namespace CBTW.Microservices.HumanResources.Application.CallCenterService;

public class ConsultarEmpleadoHandler : IRequestHandler<ConsultarEmpleadoRequest, ConsultarEmpleadoResponse>
{
	private readonly IMapper mapper;
	private readonly IGraphClient graphClient;

	public ConsultarEmpleadoHandler(IMapper mapper,
		IGraphClient graphClient)
	{
		this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		this.graphClient = graphClient ?? throw new ArgumentNullException(nameof(graphClient));
	}

	public async Task<ConsultarEmpleadoResponse> Handle(ConsultarEmpleadoRequest request, CancellationToken cancellationToken)
	{
		var employees = await this.graphClient.Cypher.Match("(e: Employee)")
													.Where((EmployeeEntity e) => e.DocumentType == request.TipoDocumento && e.Document == request.Documento)
													.Return(e => e.As<EmployeeEntity>()).ResultsAsync;

		var employee = employees.LastOrDefault();

		if (employee == null)
			throw new InvalidOperationException($"Empleado no existe!");

		var result = this.mapper.Map<EmployeeEntity, ConsultarEmpleadoResponse>(employee);

		return result;
	}
}