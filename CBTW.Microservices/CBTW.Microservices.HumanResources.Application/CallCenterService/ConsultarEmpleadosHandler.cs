using AutoMapper;
using CBTW.Microservices.HumanResources.Domain.HumanResources;
using CBTW.Microservices.HumanResources.Service.Requests;
using CBTW.Microservices.HumanResources.Service.Responses;
using MediatR;
using Neo4jClient;

namespace CBTW.Microservices.HumanResources.Application.CallCenterService;

public class ConsultarEmpleadosHandler : IRequestHandler<ConsultarEmpleadosRequest, ConsultarEmpleadosResponse>
{
	private readonly IMapper mapper;
	private readonly IGraphClient graphClient;

	public ConsultarEmpleadosHandler(IMapper mapper,
		IGraphClient graphClient)
	{
		this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		this.graphClient = graphClient ?? throw new ArgumentNullException(nameof(graphClient));
	}

	public async Task<ConsultarEmpleadosResponse> Handle(ConsultarEmpleadosRequest request, CancellationToken cancellationToken)
	{
		var employees = await this.graphClient.Cypher.Match("(e: Employee)")
													.Return(e => e.As<EmployeeEntity>()).ResultsAsync;

		var result = new ConsultarEmpleadosResponse();
		result.Empleados = new List<EmpleadoResponse>();
		foreach (var employee in employees)
		{
			result.Empleados.Add(this.mapper.Map<EmployeeEntity, EmpleadoResponse>(employee));
		}		

		return result;
	}
}