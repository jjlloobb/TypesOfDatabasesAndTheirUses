using AutoMapper;
using CBTW.Microservices.CallCenter.Application.Providers;
using CBTW.Microservices.CallCenter.Domain.CallCenter;
using CBTW.Microservices.CallCenter.Service.Requests;
using CBTW.Microservices.CallCenter.Service.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CBTW.Microservices.CallCenter.Application.CallCenterService;

public class ConsultarTelefonosCCHandler : IRequestHandler<ConsultarTelefonosCCRequest, ConsultarTelefonosCCResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public ConsultarTelefonosCCHandler(IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ConsultarTelefonosCCResponse> Handle(ConsultarTelefonosCCRequest request, CancellationToken cancellationToken)
    {
        var phonesCC = await this.unitOfWork.Phones
                                            .FindAll()
                                            .ToListAsync(cancellationToken)
                                            .ConfigureAwait(false);

        var result = new ConsultarTelefonosCCResponse();
        result.TelefonosCC = new List<TelefonoCCResponse>();
        foreach (var phone in phonesCC)
        {
            result.TelefonosCC.Add(this.mapper.Map<PhoneCCEntity, TelefonoCCResponse>(phone));
        }

        return result;
    }
}
