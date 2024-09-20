using AutoMapper;
using CBTW.Microservices.CallCenter.Application.Providers;
using CBTW.Microservices.CallCenter.Domain.CallCenter;
using CBTW.Microservices.CallCenter.Service.Requests;
using CBTW.Microservices.CallCenter.Service.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CBTW.Microservices.CallCenter.Application.CallCenterService;

public class CrearPQRHandler : IRequestHandler<CrearPQRRequest, CrearPQRResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CrearPQRHandler(IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<CrearPQRResponse> Handle(CrearPQRRequest request, CancellationToken cancellationToken)
    {
        var tipoDeDocumento = int.Parse(request.Customer.Split('@')[0] ?? "0");
        var documento = request.Customer.Split('@')[1] ?? string.Empty;

        var customer = await this.unitOfWork.Customers
                                        .FindBy(j => j.IdDocumentType == tipoDeDocumento
                                                && j.Document == documento)
                                        .FirstOrDefaultAsync(cancellationToken)
                                        .ConfigureAwait(false);

        if (customer == null)
            throw new InvalidOperationException($"Cliente no existe!");

        var newPQR = this.mapper.Map<CrearPQRRequest, PQREntity>(request);
        newPQR.IdCustomer = customer.Id;
        this.unitOfWork.PQRs.Create(newPQR);
        await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);

        return new CrearPQRResponse();                                    
    }
}
