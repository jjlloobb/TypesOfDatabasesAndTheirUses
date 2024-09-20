using AutoMapper;
using CBTW.Microservices.CallCenter.Application.Providers;
using CBTW.Microservices.CallCenter.Domain.CallCenter;
using CBTW.Microservices.CallCenter.Service.Requests;
using CBTW.Microservices.CallCenter.Service.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CBTW.Microservices.CallCenter.Application.CallCenterService;

public class ConsultarPQRHandler : IRequestHandler<ConsultarPQRRequest, ConsultarPQRResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public ConsultarPQRHandler(IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ConsultarPQRResponse> Handle(ConsultarPQRRequest request, CancellationToken cancellationToken)
    {
        var pqr = new PQREntity();

        if (request.Id > 0)
        {
            pqr = await this.unitOfWork.PQRs
                                        .FindBy(j => j.Id == request.Id)
                                        .FirstOrDefaultAsync(cancellationToken)
                                        .ConfigureAwait(false);
        }
        else
        {
            pqr = await this.unitOfWork.PQRs
                                            .FindBy(j => j.IdCustomer == request.IdCustomer)
                                            .FirstOrDefaultAsync(cancellationToken)
                                            .ConfigureAwait(false);
        }

        if (pqr == null)
            throw new InvalidOperationException($"Cliente no existe!");

        var result = this.mapper.Map<PQREntity, ConsultarPQRResponse>(pqr);

        return result;
    }
}
