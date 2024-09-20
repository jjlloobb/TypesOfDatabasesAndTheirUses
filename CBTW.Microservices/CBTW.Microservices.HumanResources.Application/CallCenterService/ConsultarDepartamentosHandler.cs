using AutoMapper;
using CBTW.Microservices.HumanResources.Domain.HumanResources;
using CBTW.Microservices.HumanResources.Service.Requests;
using CBTW.Microservices.HumanResources.Service.Responses;
using MediatR;
using Neo4jClient;

namespace CBTW.Microservices.HumanResources.Application.CallCenterService;

public class ConsultarDepartamentosHandler : IRequestHandler<ConsultarDepartamentosRequest, ConsultarDepartamentosResponse>
{
    private readonly IMapper mapper;
	private readonly IGraphClient graphClient;

	public ConsultarDepartamentosHandler(IMapper mapper,
		IGraphClient graphClient)
	{
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		this.graphClient = graphClient ?? throw new ArgumentNullException(nameof(graphClient));
	}

    public async Task<ConsultarDepartamentosResponse> Handle(ConsultarDepartamentosRequest request, CancellationToken cancellationToken)
    {
		var departments = await this.graphClient.Cypher.Match("(d: Department)")
													.Return(d => d.As<DepartmentEntity>()).ResultsAsync;

		var result = new ConsultarDepartamentosResponse();
		result.Departamentos = new List<DepartamentoResponse>();
		foreach (var department in departments)
		{
			result.Departamentos.Add(this.mapper.Map<DepartmentEntity, DepartamentoResponse>(department));
		}

		return result;
	}
}