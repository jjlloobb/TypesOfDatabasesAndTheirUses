using AutoMapper;
using CBTW.Microservices.HumanResources.Domain.HumanResources;
using CBTW.Microservices.HumanResources.Service.Requests;
using CBTW.Microservices.HumanResources.Service.Responses;
using MediatR;
using Neo4jClient;

namespace CBTW.Microservices.HumanResources.Application.CallCenterService;

public class CrearDepartamentoHandler : IRequestHandler<CrearDepartamentoRequest, CrearDepartamentoResponse>
{
	private readonly IMapper mapper;
	private readonly IGraphClient graphClient;

	public CrearDepartamentoHandler(IMapper mapper,
		IGraphClient graphClient)
	{
		this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		this.graphClient = graphClient ?? throw new ArgumentNullException(nameof(graphClient));
	}

	public async Task<CrearDepartamentoResponse> Handle(CrearDepartamentoRequest request, CancellationToken cancellationToken)
	{
		var departments = await this.graphClient.Cypher.Match("(d: Department)")
													.Where((DepartmentEntity d) => d.Abbreviation == request.Abreviacion)
													.Return(d => d.As<DepartmentEntity>()).ResultsAsync;

		var department = departments.LastOrDefault();

		if (department != null)
		{
			department.FullName = request.NombreCompleto;
			department.Description = request.Descripcion;
			department.CountryCode = request.CodigoPaisCelular;
			department.PhoneNumber = request.Celular;
			department.UpdateDate = DateTime.Now;

			return new CrearDepartamentoResponse();
		}

		var departmentEntity = this.mapper.Map<CrearDepartamentoRequest, DepartmentEntity>(request);

		await this.graphClient.Cypher.Create("(d: Department $request)")
									.WithParam("request", departmentEntity)
									.ExecuteWithoutResultsAsync();

		return new CrearDepartamentoResponse();
	}
}