using AutoMapper;
using CBTW.Microservices.HumanResources.Domain.HumanResources;
using CBTW.Microservices.HumanResources.Service.Requests;
using CBTW.Microservices.HumanResources.Service.Responses;
using MediatR;
using Neo4jClient;

namespace CBTW.Microservices.HumanResources.Application.CallCenterService;

public class CrearEmpleadoHandler : IRequestHandler<CrearEmpleadoRequest, CrearEmpleadoResponse>
{
	private readonly IMapper mapper;
	private readonly IGraphClient graphClient;

	public CrearEmpleadoHandler(IMapper mapper,
		IGraphClient graphClient)
	{
		this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		this.graphClient = graphClient ?? throw new ArgumentNullException(nameof(graphClient));
	}

	public async Task<CrearEmpleadoResponse> Handle(CrearEmpleadoRequest request, CancellationToken cancellationToken)
	{
		var employees = await this.graphClient.Cypher.Match("(e: Employee)")
													.Where((EmployeeEntity e) => e.DocumentType == request.TipoDocumento && e.Document == request.Documento)
													.Return(e => e.As<EmployeeEntity>()).ResultsAsync;

		var employee = employees.LastOrDefault();

		if (employee != null)
		{
			employee.FullName = request.NombreCompleto;
			employee.CountryCode = request.CodigoPaisCelular;
			employee.PhoneNumber = request.Celular;
			employee.DateOfBirth = request.FechaNacimiento;
			employee.UpdateDate = DateTime.Now;

			return new CrearEmpleadoResponse();
		}

		var employeeEntity = this.mapper.Map<CrearEmpleadoRequest, EmployeeEntity>(request);

		await this.graphClient.Cypher.Create("(e: Employee $request)")
									.WithParam("request", employeeEntity)
									.ExecuteWithoutResultsAsync();

		return new CrearEmpleadoResponse();
	}
}