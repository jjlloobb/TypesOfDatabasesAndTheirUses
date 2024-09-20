using CBTW.Microservices.CallCenter.Application.Providers;
using CBTW.Microservices.CallCenter.Service.Requests;
using CBTW.Microservices.CallCenter.Service.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CBTW.Microservices.CallCenter.Application.CallCenterService;

public class ActualizarClienteHandler : IRequestHandler<ActualizarClienteRequest, ActualizarClienteResponse>
{
    private readonly IUnitOfWork unitOfWork;

    public ActualizarClienteHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ActualizarClienteResponse> Handle(ActualizarClienteRequest request, CancellationToken cancellationToken)
    {
        var customer = await this.unitOfWork.Customers
                                        .FindBy(j => j.IdDocumentType == request.TipoDocumento
                                                && j.Document == request.Documento)
                                        .FirstOrDefaultAsync(cancellationToken)
                                        .ConfigureAwait(false);

        if (customer == null)
            throw new InvalidOperationException($"Cliente no existe!");

        customer.FullName = request.NombreCompleto;
        customer.CountryCode = request.CodigoPaisCelular;
        customer.PhoneNumber = request.Celular;
        customer.IdCity = request.Ciudad;
        customer.DateOfBirth = request.FechaNacimiento;
        customer.IdPhoneCC = request.TelefonoCC;
        customer.UpdateDate = DateTime.Now;

        this.unitOfWork.Customers.Update(customer);
        await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);

        return new ActualizarClienteResponse();
    }
}
