using AutoMapper;
using CBTW.Microservices.CallCenter.Application.Providers;
using CBTW.Microservices.CallCenter.Domain.CallCenter;
using CBTW.Microservices.CallCenter.Domain.Enums;
using CBTW.Microservices.CallCenter.Service.Requests;
using CBTW.Microservices.CallCenter.Service.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace CBTW.Microservices.CallCenter.Application.CallCenterService;

public class ConsultarCiudadesHandler : IRequestHandler<ConsultarCiudadesRequest, ConsultarCiudadesResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
	private readonly IMemoryProvider memoryProvider;

	public ConsultarCiudadesHandler(IUnitOfWork unitOfWork,
        IMapper mapper,
		IMemoryProvider memoryProvider)
    {
        this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		this.memoryProvider = memoryProvider ?? throw new ArgumentNullException(nameof(memoryProvider));
	}

    public async Task<ConsultarCiudadesResponse> Handle(ConsultarCiudadesRequest request, CancellationToken cancellationToken)
    {
		var result = new ConsultarCiudadesResponse();
		result.Ciudades = new List<CiudadResponse>();

		//Gey key in distributed cache
		var citiesCache = await this.memoryProvider.GetCacheValuesAsync<CityEntity>(
					nameof(ProcessNameEnum.ConsultarCiudadesHandler),
					cancellationToken)
					.ConfigureAwait(false);

        if (citiesCache != null)
        {
			foreach (var city in citiesCache)
			{
				result.Ciudades.Add(this.mapper.Map<CityEntity, CiudadResponse>(city));
			}

			return result;
		}

		var cities = await this.unitOfWork.Cities
                                    .FindAll()
                                    .ToListAsync(cancellationToken)
                                    .ConfigureAwait(false);
        
        foreach (var city in cities)
        {
            result.Ciudades.Add(this.mapper.Map<CityEntity, CiudadResponse>(city));
        }

		//Insert a key in distributed cache
		await this.memoryProvider.SetCacheValuesAsync<CityEntity>(
			nameof(ProcessNameEnum.ConsultarCiudadesHandler),
			cities,
			cancellationToken,
			distributedCacheExpiration: true)
			.ConfigureAwait(false);

		return result;
    }
}
