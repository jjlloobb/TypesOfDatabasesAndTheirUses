using AutoMapper;
using CBTW.Microservices.CallCenter.Application.Providers;
using CBTW.Microservices.CallCenter.Domain.CallCenter;
using CBTW.Microservices.CallCenter.Service.Requests;
using CBTW.Microservices.CallCenter.Service.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CBTW.Microservices.CallCenter.Application.CallCenterService;

public class ConsultarPQRsHandler : IRequestHandler<ConsultarPQRsRequest, ConsultarPQRsResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public ConsultarPQRsHandler(IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ConsultarPQRsResponse> Handle(ConsultarPQRsRequest request, CancellationToken cancellationToken)
    {
        var pqrs = await this.unitOfWork.PQRs
                                        .FindAll()
                                        .ToListAsync(cancellationToken)
                                        .ConfigureAwait(false);

        var result = new ConsultarPQRsResponse();
        result.PQRs = new List<PQRResponse>();
        foreach (var pqr in pqrs)
        {
            result.PQRs.Add(this.mapper.Map<PQREntity, PQRResponse>(pqr));
        }

        return result;
    }
}
