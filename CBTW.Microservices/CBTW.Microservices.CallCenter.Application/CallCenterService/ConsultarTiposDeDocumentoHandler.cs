using AutoMapper;
using CBTW.Microservices.CallCenter.Application.Providers;
using CBTW.Microservices.CallCenter.Domain.CallCenter;
using CBTW.Microservices.CallCenter.Domain.Enums;
using CBTW.Microservices.CallCenter.Service.Requests;
using CBTW.Microservices.CallCenter.Service.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CBTW.Microservices.CallCenter.Application.CallCenterService;

public class ConsultarTiposDeDocumentoHandler : IRequestHandler<ConsultarTiposDeDocumentoRequest, ConsultarTiposDeDocumentoResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
	private readonly IMemoryProvider memoryProvider;

	public ConsultarTiposDeDocumentoHandler(IUnitOfWork unitOfWork,
        IMapper mapper,
		IMemoryProvider memoryProvider)
    {
        this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		this.memoryProvider = memoryProvider ?? throw new ArgumentNullException(nameof(memoryProvider));
	}

    public async Task<ConsultarTiposDeDocumentoResponse> Handle(ConsultarTiposDeDocumentoRequest request, CancellationToken cancellationToken)
    {
		var result = new ConsultarTiposDeDocumentoResponse();
		result.TiposDeDocumento = new List<TiposDeDocumentoResponse>();

		//Gey key in distributed cache
		var documentTypesCache = await this.memoryProvider.GetCacheValuesAsync<DocumentTypeEntity>(
					nameof(ProcessNameEnum.ConsultarTiposDeDocumentoHandler),
					cancellationToken)
					.ConfigureAwait(false);

        if (documentTypesCache != null)
        {
			foreach (var documentType in documentTypesCache)
			{
				result.TiposDeDocumento.Add(this.mapper.Map<DocumentTypeEntity, TiposDeDocumentoResponse>(documentType));
			}

			return result;
		}

		var documentTypes = await this.unitOfWork.DocumentTypes
                                            .FindAll()
                                            .ToListAsync(cancellationToken)
                                            .ConfigureAwait(false);
        
        foreach (var documentType in documentTypes)
        {
            result.TiposDeDocumento.Add(this.mapper.Map<DocumentTypeEntity, TiposDeDocumentoResponse>(documentType));
        }

		//Insert a key in distributed cache
		await this.memoryProvider.SetCacheValuesAsync<DocumentTypeEntity>(
			nameof(ProcessNameEnum.ConsultarTiposDeDocumentoHandler),
			documentTypes,
			cancellationToken,
			distributedCacheExpiration: true)
			.ConfigureAwait(false);

		return result;
    }
}
