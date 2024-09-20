using AutoMapper;
using CBTW.Microservices.CallCenter.Application.Providers;
using CBTW.Microservices.CallCenter.Domain.CallCenter;
using CBTW.Microservices.CallCenter.Service.Requests;
using CBTW.Microservices.CallCenter.Service.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CBTW.Microservices.CallCenter.Application.CallCenterService;

public class ConsultarClienteHandler : IRequestHandler<ConsultarClienteRequest, ConsultarClienteResponse>
{
	private readonly IUnitOfWork unitOfWork;
	private readonly IMapper mapper;

	public ConsultarClienteHandler(IUnitOfWork unitOfWork,
		IMapper mapper)
	{
		this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
	}

	public async Task<ConsultarClienteResponse> Handle(ConsultarClienteRequest request, CancellationToken cancellationToken)
	{
		var customer = new CustomerEntity();

		if (request.Id > 0)
		{
			customer = await this.unitOfWork.Customers
										.FindBy(j => j.Id == request.Id)
										.FirstOrDefaultAsync(cancellationToken)
										.ConfigureAwait(false);
		}
		else
		{
			customer = await this.unitOfWork.Customers
											.FindBy(j => j.IdDocumentType == request.TipoDocumento
													&& j.Document == request.Documento)
											.FirstOrDefaultAsync(cancellationToken)
											.ConfigureAwait(false);
		}

		if (customer == null)
			throw new InvalidOperationException($"Cliente no existe!");

		var result = this.mapper.Map<CustomerEntity, ConsultarClienteResponse>(customer);

		return result;
	}
}