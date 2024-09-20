using AutoMapper;
using CBTW.Microservices.HumanResources.Domain.HumanResources;
using CBTW.Microservices.HumanResources.Service.Requests;
using CBTW.Microservices.HumanResources.Service.Responses;
using MediatR;
using Neo4jClient;

namespace CBTW.Microservices.HumanResources.Application.CallCenterService;

public class ConsultarDepartamentoHandler : IRequestHandler<ConsultarDepartamentoRequest, ConsultarDepartamentoResponse>
{
	private readonly IMapper mapper;
	private readonly IGraphClient graphClient;

	public ConsultarDepartamentoHandler(IMapper mapper,
		IGraphClient graphClient)
	{
		this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		this.graphClient = graphClient ?? throw new ArgumentNullException(nameof(graphClient));
	}

	public async Task<ConsultarDepartamentoResponse> Handle(ConsultarDepartamentoRequest request, CancellationToken cancellationToken)
	{
		var departments = await this.graphClient.Cypher.Match("(d: Department)")
													.Where((DepartmentEntity d) => d.Abbreviation == request.Abreviacion)
													.Return(d => d.As<DepartmentEntity>()).ResultsAsync;

		var department = departments.LastOrDefault();

		if (department == null)
			throw new InvalidOperationException($"Departamento no existe!");

		var result = this.mapper.Map<DepartmentEntity, ConsultarDepartamentoResponse>(department);

		return result;
	}
}