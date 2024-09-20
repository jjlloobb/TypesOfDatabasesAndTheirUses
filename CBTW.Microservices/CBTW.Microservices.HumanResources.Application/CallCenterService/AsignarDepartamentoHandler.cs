using AutoMapper;
using CBTW.Microservices.HumanResources.Domain.HumanResources;
using CBTW.Microservices.HumanResources.Service.Requests;
using CBTW.Microservices.HumanResources.Service.Responses;
using MediatR;
using Neo4jClient;

namespace CBTW.Microservices.HumanResources.Application.CallCenterService;

public class AsignarDepartamentoHandler : IRequestHandler<AsignarDepartamentoRequest, AsignarDepartamentoResponse>
{
	private readonly IMapper mapper;
	private readonly IGraphClient graphClient;

	public AsignarDepartamentoHandler(IMapper mapper,
		IGraphClient graphClient)
	{
		this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		this.graphClient = graphClient ?? throw new ArgumentNullException(nameof(graphClient));
	}

	public async Task<AsignarDepartamentoResponse> Handle(AsignarDepartamentoRequest request, CancellationToken cancellationToken)
	{
		var employees = await this.graphClient.Cypher.Match("(e: Employee)")
													.Where((EmployeeEntity e) => e.DocumentType == request.TipoDocumento && e.Document == request.Documento)
													.Return(e => e.As<EmployeeEntity>()).ResultsAsync;

		var employee = employees.LastOrDefault();

		if (employee == null)
			throw new InvalidOperationException($"Empleado no existe!");

		var departments = await this.graphClient.Cypher.Match("(d: Department)")
													.Where((DepartmentEntity d) => d.Abbreviation == request.Abreviacion)
													.Return(d => d.As<DepartmentEntity>()).ResultsAsync;

		var department = departments.LastOrDefault();

		if (department == null)
			throw new InvalidOperationException($"Departamento no existe!");

		await this.graphClient.Cypher.Match("(d: Department), (e: Employee)")
									.Where((DepartmentEntity d, EmployeeEntity e) => d.Abbreviation == request.Abreviacion && e.DocumentType == request.TipoDocumento && e.Document == request.Documento)
									.Create("(d)-[r:hasEmployee]->(e)")
									.ExecuteWithoutResultsAsync();

		return new AsignarDepartamentoResponse();
	}
}