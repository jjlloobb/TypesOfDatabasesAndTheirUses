using AutoMapper;
using CBTW.Microservices.CallCenter.Application.Providers;
using CBTW.Microservices.CallCenter.Domain.CallCenter;
using CBTW.Microservices.CallCenter.Service.Requests;
using CBTW.Microservices.CallCenter.Service.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CBTW.Microservices.CallCenter.Application.CallCenterService;

public class ConsultarClientesHandler : IRequestHandler<ConsultarClientesRequest, ConsultarClientesResponse>
{
	private readonly IUnitOfWork unitOfWork;
	private readonly IMapper mapper;

	public ConsultarClientesHandler(IUnitOfWork unitOfWork,
		IMapper mapper)
	{
		this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
	}

	public async Task<ConsultarClientesResponse> Handle(ConsultarClientesRequest request, CancellationToken cancellationToken)
	{
		var customers = await this.unitOfWork.Customers
										.FindAll()
										.ToListAsync(cancellationToken)
										.ConfigureAwait(false);

		var result = new ConsultarClientesResponse();
		result.Clientes = new List<ClienteResponse>();
		foreach (var customer in customers)
		{
			result.Clientes.Add(this.mapper.Map<CustomerEntity, ClienteResponse>(customer));
		}

		return result;
	}
}