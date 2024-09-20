using AutoMapper;
using CBTW.Microservices.HumanResources.Domain.HumanResources;
using CBTW.Microservices.HumanResources.Service.Requests;
using CBTW.Microservices.HumanResources.Service.Responses;
using MediatR;
using Neo4jClient;

namespace CBTW.Microservices.HumanResources.Application.CallCenterService;

public class ActualizarDepartamentoHandler : IRequestHandler<ActualizarDepartamentoRequest, ActualizarDepartamentoResponse>
{
	private readonly IMapper mapper;
	private readonly IGraphClient graphClient;

	public ActualizarDepartamentoHandler(IMapper mapper,
		IGraphClient graphClient)
	{
		this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		this.graphClient = graphClient ?? throw new ArgumentNullException(nameof(graphClient));
	}

	public async Task<ActualizarDepartamentoResponse> Handle(ActualizarDepartamentoRequest request, CancellationToken cancellationToken)
	{
		var departments = await this.graphClient.Cypher.Match("(d: Department)")
													.Where((DepartmentEntity d) => d.Abbreviation == request.Abreviacion)
													.Return(d => d.As<DepartmentEntity>()).ResultsAsync;

		var department = departments.LastOrDefault();

		if (department == null)
			throw new InvalidOperationException($"Departamento no existe!");

		department.FullName = request.NombreCompleto;
		department.Description = request.Descripcion;
		department.CountryCode = request.CodigoPaisCelular;
		department.PhoneNumber = request.Celular;
		department.UpdateDate = DateTime.Now;

		await this.graphClient.Cypher.Match("(d: Department)")
									.Where((DepartmentEntity d) => d.Abbreviation == request.Abreviacion)
									.Set("d = $request")
									.WithParam("request", department)
									.ExecuteWithoutResultsAsync();

		return new ActualizarDepartamentoResponse();
	}
}