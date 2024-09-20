using AutoMapper;
using CBTW.Microservices.CallCenter.Application.Providers;
using CBTW.Microservices.CallCenter.Domain.CallCenter;
using CBTW.Microservices.CallCenter.Service.Requests;
using CBTW.Microservices.CallCenter.Service.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CBTW.Microservices.CallCenter.Application.CallCenterService;

public class CrearClienteHandler : IRequestHandler<CrearClienteRequest, CrearClienteResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CrearClienteHandler(IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<CrearClienteResponse> Handle(CrearClienteRequest request, CancellationToken cancellationToken)
    {
        var customer = await this.unitOfWork.Customers
                                        .FindBy(j => j.IdDocumentType == request.TipoDocumento
                                                && j.Document == request.Documento)
                                        .FirstOrDefaultAsync(cancellationToken)
                                        .ConfigureAwait(false);

        if (customer != null)
        {
            customer.FullName = request.NombreCompleto;
            customer.CountryCode = request.CodigoPaisCelular;
            customer.PhoneNumber = request.Celular;
            customer.IdCity = request.Ciudad;
            customer.DateOfBirth = request.FechaNacimiento;
            customer.IdPhoneCC = request.TelefonoCC;
            customer.UpdateDate = DateTime.Now;

            this.unitOfWork.Customers.Update(customer);
            await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);

            return new CrearClienteResponse();
        }

        var newCustomer = this.mapper.Map<CrearClienteRequest, CustomerEntity>(request);
        this.unitOfWork.Customers.Create(newCustomer);
        await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);

        return new CrearClienteResponse();
    }
}
