using AutoMapper;
using CBTW.Microservices.UI.Application.Providers;
using CBTW.Microservices.UI.Domain.CallCenter.ActualizarCliente;
using CBTW.Microservices.UI.Domain.CallCenter.ConsultarCiudades;
using CBTW.Microservices.UI.Domain.CallCenter.ConsultarClientes;
using CBTW.Microservices.UI.Domain.CallCenter.ConsultarTelefonosCC;
using CBTW.Microservices.UI.Domain.CallCenter.ConsultarTiposDeDocumento;
using CBTW.Microservices.UI.Domain.Enums;
using CBTW.Microservices.UI.Domain.Models;
using CBTW.Microservices.UI.Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Net;
using System.Reflection;

namespace CBTW.Microservices.UI.Infrastructure.Providers;

public class CallCenterApiProvider : ICallCenterProvider
{
	private readonly ILogger<CallCenterApiProvider> logger;

	private readonly IMapper mapper;

	private readonly IHttpClientFactory httpClientFactory;

	private readonly HttpClient httpClient;

	private readonly CallCenter.ICallCenterClient callCenterClient;

	private readonly IDocumentaryDbProvider documentaryDbProvider;

	public CallCenterApiProvider(ILogger<CallCenterApiProvider> logger,
		IMapper mapper,
		IHttpClientFactory httpClientFactory,
		IDocumentaryDbProvider documentaryDbProvider)
	{
		this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
		this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
		this.httpClient = this.httpClientFactory.CreateClient("CallCenterProvider");
		this.callCenterClient = new CallCenter.CallCenterClient(this.httpClient);
		this.documentaryDbProvider = documentaryDbProvider ?? throw new ArgumentNullException(nameof(documentaryDbProvider));
	}

	public async Task<List<CustomerViewModel>> ConsultarClientes(CancellationToken cancellationToken)
	{
		var response = await this.CallService<CallCenter.ConsultarClientesResponse, CallCenter.ConsultarClientesResponse>
			(this.callCenterClient.ConsultarClientesAsync,
			nameof(this.callCenterClient.ConsultarClientesAsync),
			cancellationToken)
			.ConfigureAwait(false);

		var result = new List<CustomerViewModel>();
		if (response.Clientes.Any())
		{
			var responseTiposDeDocumentos = await this.CallService<CallCenter.ConsultarTiposDeDocumentoResponse, CallCenter.ConsultarTiposDeDocumentoResponse>
				(this.callCenterClient.ConsultarTiposDeDocumentoAsync,
				nameof(this.callCenterClient.ConsultarTiposDeDocumentoAsync),
				cancellationToken)
				.ConfigureAwait(false);

			var responseCiudades = await this.CallService<CallCenter.ConsultarCiudadesResponse, CallCenter.ConsultarCiudadesResponse>
				(this.callCenterClient.ConsultarCiudadesAsync,
				nameof(this.callCenterClient.ConsultarCiudadesAsync),
				cancellationToken)
				.ConfigureAwait(false);

			var responseTelefonosCC = await this.CallService<CallCenter.ConsultarTelefonosCCResponse, CallCenter.ConsultarTelefonosCCResponse>
				(this.callCenterClient.ConsultarTelefonosCCAsync,
				nameof(this.callCenterClient.ConsultarTelefonosCCAsync),
				cancellationToken)
				.ConfigureAwait(false);

			foreach (var cliente in response.Clientes)
			{
				var newCliente = this.mapper.Map<CallCenter.ClienteResponse, CustomerViewModel>(cliente);

				var tipoDocumento = responseTiposDeDocumentos.TiposDeDocumento.FirstOrDefault(j => j.Id == newCliente.IdTipoDocumento);
				newCliente.TipoDocumento = tipoDocumento != null ? $"{tipoDocumento.Name} {tipoDocumento.Descripcion}" : throw new Exception("TipoDocumento no existe!");

				newCliente.Ciudad = responseCiudades.Ciudades.FirstOrDefault(j => j.Id == newCliente.IdCiudad)?.Name ?? throw new Exception("Ciudad no existe!");

				var telefonoCC = responseTelefonosCC.TelefonosCC.FirstOrDefault(j => j.Id == newCliente.IdTelefonoCC);
				newCliente.TelefonoCC = telefonoCC != null ? $"{telefonoCC.CodigoPaisCelular} {telefonoCC.Celular}" : throw new Exception("TelefonoCC no existe!");

				result.Add(newCliente);
			}
		}

		return result;
	}

	public async Task<List<ConsultarClientesRespuestaValue>> ConsultarClientesRegistrados(CancellationToken cancellationToken)
	{
		var response = await this.CallService<CallCenter.ConsultarPQRsResponse, CallCenter.ConsultarPQRsResponse>
			(this.callCenterClient.ConsultarPQRsAsync,
			nameof(this.callCenterClient.ConsultarPQRsAsync),
			cancellationToken)
			.ConfigureAwait(false);

		var result = new List<ConsultarClientesRespuestaValue>();
		foreach (var pqr in response.PqRs.DistinctBy(j => j.IdCustomer))
		{
			var responseCliente = await this.CallService<CallCenter.PQRResponse, CallCenter.ConsultarClienteResponse,
				CallCenter.ConsultarClienteRequest, CallCenter.ConsultarClienteResponse>
				(pqr,
				this.callCenterClient.ConsultarClienteAsync,
				nameof(this.callCenterClient.ConsultarClienteAsync),
				cancellationToken)
				.ConfigureAwait(false);

			result.Add(this.mapper.Map<CallCenter.ConsultarClienteResponse, ConsultarClientesRespuestaValue>(responseCliente));
		}

		return result;
	}

	public async Task<CustomerViewModel> ConsultarCliente(CustomerViewModel customerViewModel, CancellationToken cancellationToken)
	{
		var response = await this.CallService<CustomerViewModel, CallCenter.ConsultarClienteResponse,
				CallCenter.ConsultarClienteRequest, CallCenter.ConsultarClienteResponse>
				(customerViewModel,
				this.callCenterClient.ConsultarClienteAsync,
				nameof(this.callCenterClient.ConsultarClienteAsync),
				cancellationToken)
				.ConfigureAwait(false);

		var responseTiposDeDocumentos = await this.CallService<CallCenter.ConsultarTiposDeDocumentoResponse, CallCenter.ConsultarTiposDeDocumentoResponse>
				(this.callCenterClient.ConsultarTiposDeDocumentoAsync,
				nameof(this.callCenterClient.ConsultarTiposDeDocumentoAsync),
				cancellationToken)
				.ConfigureAwait(false);

		var responseCiudades = await this.CallService<CallCenter.ConsultarCiudadesResponse, CallCenter.ConsultarCiudadesResponse>
			(this.callCenterClient.ConsultarCiudadesAsync,
			nameof(this.callCenterClient.ConsultarCiudadesAsync),
			cancellationToken)
			.ConfigureAwait(false);

		var responseTelefonosCC = await this.CallService<CallCenter.ConsultarTelefonosCCResponse, CallCenter.ConsultarTelefonosCCResponse>
			(this.callCenterClient.ConsultarTelefonosCCAsync,
			nameof(this.callCenterClient.ConsultarTelefonosCCAsync),
			cancellationToken)
			.ConfigureAwait(false);

		var result = this.mapper.Map<CallCenter.ConsultarClienteResponse, CustomerViewModel>(response);

		var tipoDocumento = responseTiposDeDocumentos.TiposDeDocumento.FirstOrDefault(j => j.Id == result.IdTipoDocumento);
		result.TipoDocumento = tipoDocumento != null ? $"{tipoDocumento.Name} {tipoDocumento.Descripcion}" : throw new Exception("TipoDocumento no existe!");

		result.Ciudad = responseCiudades.Ciudades.FirstOrDefault(j => j.Id == result.IdCiudad)?.Name ?? throw new Exception("Ciudad no existe!");

		var telefonoCC = responseTelefonosCC.TelefonosCC.FirstOrDefault(j => j.Id == result.IdTelefonoCC);
		result.TelefonoCC = telefonoCC != null ? $"{telefonoCC.CodigoPaisCelular} {telefonoCC.Celular}" : throw new Exception("TelefonoCC no existe!");

		return result;
	}

	public async Task CrearCliente(CustomerViewModel customerViewModel, CancellationToken cancellationToken)
	{
		try
		{
			var response = await this.CallService<CustomerViewModel, CallCenter.ConsultarClienteResponse,
				CallCenter.ConsultarClienteRequest, CallCenter.ConsultarClienteResponse>
				(customerViewModel,
				this.callCenterClient.ConsultarClienteAsync,
				nameof(this.callCenterClient.ConsultarClienteAsync),
				cancellationToken)
				.ConfigureAwait(false);

			var cliente = this.mapper.Map<CallCenter.ConsultarClienteResponse, ActualizarClienteValue>(response);

			var result = await this.CallService<ActualizarClienteValue, CallCenter.ActualizarClienteResponse,
				CallCenter.ActualizarClienteRequest, CallCenter.ActualizarClienteResponse>
				(cliente,
				this.callCenterClient.ActualizarClienteAsync,
				nameof(this.callCenterClient.ActualizarClienteAsync),
				cancellationToken)
				.ConfigureAwait(false);
		}
		catch (InvalidOperationException ioex)
		{
			var result = await this.CallService<CustomerViewModel, CallCenter.CrearClienteResponse,
				CallCenter.CrearClienteRequest, CallCenter.CrearClienteResponse>
				(customerViewModel,
				this.callCenterClient.CrearClienteAsync,
				nameof(this.callCenterClient.CrearClienteAsync),
				cancellationToken)
				.ConfigureAwait(false);
		}
	}

	public async Task ActualizarCliente(CustomerViewModel customerViewModel, CancellationToken cancellationToken)
	{
		customerViewModel.Id = null;

		var response = await this.CallService<CustomerViewModel, CallCenter.ConsultarClienteResponse,
				CallCenter.ConsultarClienteRequest, CallCenter.ConsultarClienteResponse>
				(customerViewModel,
				this.callCenterClient.ConsultarClienteAsync,
				nameof(this.callCenterClient.ConsultarClienteAsync),
				cancellationToken)
				.ConfigureAwait(false);

		var cliente = this.mapper.Map<CustomerViewModel, ActualizarClienteValue>(customerViewModel);

		var result = await this.CallService<ActualizarClienteValue, CallCenter.ActualizarClienteResponse,
			CallCenter.ActualizarClienteRequest, CallCenter.ActualizarClienteResponse>
			(cliente,
			this.callCenterClient.ActualizarClienteAsync,
			nameof(this.callCenterClient.ActualizarClienteAsync),
			cancellationToken)
			.ConfigureAwait(false);
	}

	public async Task<List<TiposDeDocumentoRespuestaValue>> ConsultarTiposDeDocumento(CancellationToken cancellationToken)
	{
		var response = await this.CallService<CallCenter.ConsultarTiposDeDocumentoResponse, CallCenter.ConsultarTiposDeDocumentoResponse>
				(this.callCenterClient.ConsultarTiposDeDocumentoAsync,
				nameof(this.callCenterClient.ConsultarTiposDeDocumentoAsync),
				cancellationToken)
				.ConfigureAwait(false);

		var result = new List<TiposDeDocumentoRespuestaValue>();
		foreach (var TipoDeDocumento in response.TiposDeDocumento)
		{
			result.Add(this.mapper.Map<CallCenter.TiposDeDocumentoResponse, TiposDeDocumentoRespuestaValue>(TipoDeDocumento));
		}

		return result;
	}

	public async Task<List<ConsultarCiudadesRespuestaValue>> ConsultarCiudades(CancellationToken cancellationToken)
	{
		var response = await this.CallService<CallCenter.ConsultarCiudadesResponse, CallCenter.ConsultarCiudadesResponse>
				(this.callCenterClient.ConsultarCiudadesAsync,
				nameof(this.callCenterClient.ConsultarCiudadesAsync),
				cancellationToken)
				.ConfigureAwait(false);

		var result = new List<ConsultarCiudadesRespuestaValue>();
		foreach (var Ciudad in response.Ciudades)
		{
			result.Add(this.mapper.Map<CallCenter.CiudadResponse, ConsultarCiudadesRespuestaValue>(Ciudad));
		}

		return result;
	}

	public async Task<List<ConsultarTelefonosCCRespuestaValue>> ConsultarTelefonosCC(CancellationToken cancellationToken)
	{
		var response = await this.CallService<CallCenter.ConsultarTelefonosCCResponse, CallCenter.ConsultarTelefonosCCResponse>
				(this.callCenterClient.ConsultarTelefonosCCAsync,
				nameof(this.callCenterClient.ConsultarTelefonosCCAsync),
				cancellationToken)
				.ConfigureAwait(false);

		var result = new List<ConsultarTelefonosCCRespuestaValue>();
		foreach (var TelefonoCC in response.TelefonosCC)
		{
			result.Add(this.mapper.Map<CallCenter.TelefonoCCResponse, ConsultarTelefonosCCRespuestaValue>(TelefonoCC));
		}

		return result;
	}

	public async Task<List<PQRViewModel>> ConsultarPQRs(CancellationToken cancellationToken)
	{
		var response = await this.CallService<CallCenter.ConsultarPQRsResponse, CallCenter.ConsultarPQRsResponse>
			(this.callCenterClient.ConsultarPQRsAsync,
			nameof(this.callCenterClient.ConsultarPQRsAsync),
			cancellationToken)
			.ConfigureAwait(false);

		var result = new List<PQRViewModel>();
		foreach (var pqr in response.PqRs)
		{
			var newPQR = this.mapper.Map<CallCenter.PQRResponse, PQRViewModel>(pqr);

			var responseCliente = await this.CallService<CallCenter.PQRResponse, CallCenter.ConsultarClienteResponse,
				CallCenter.ConsultarClienteRequest, CallCenter.ConsultarClienteResponse>
				(pqr,
				this.callCenterClient.ConsultarClienteAsync,
				nameof(this.callCenterClient.ConsultarClienteAsync),
				cancellationToken)
				.ConfigureAwait(false);

			newPQR.Customer = $"{responseCliente.TipoDocumento}@{responseCliente.Documento}";
			newPQR.Documento = responseCliente.Documento;
			newPQR.NombreCompleto = responseCliente.NombreCompleto;

			result.Add(newPQR);
		}

		return result;
	}

	public async Task<PQRViewModel> ConsultarPQR(PQRViewModel pqrViewModel, CancellationToken cancellationToken)
	{
		var response = await this.CallService<PQRViewModel, CallCenter.ConsultarPQRResponse,
				CallCenter.ConsultarPQRRequest, CallCenter.ConsultarPQRResponse>
				(pqrViewModel,
				this.callCenterClient.ConsultarPQRAsync,
				nameof(this.callCenterClient.ConsultarPQRAsync),
				cancellationToken)
				.ConfigureAwait(false);

		var pqr = this.mapper.Map<CallCenter.ConsultarPQRResponse, CallCenter.PQRResponse>(response);

		var responseCliente = await this.CallService<CallCenter.PQRResponse, CallCenter.ConsultarClienteResponse,
			CallCenter.ConsultarClienteRequest, CallCenter.ConsultarClienteResponse>
			(pqr,
			this.callCenterClient.ConsultarClienteAsync,
			nameof(this.callCenterClient.ConsultarClienteAsync),
			cancellationToken)
			.ConfigureAwait(false);

		var result = this.mapper.Map<CallCenter.ConsultarPQRResponse, PQRViewModel>(response);
		result.Documento = responseCliente.Documento;
		result.NombreCompleto = responseCliente.NombreCompleto;

		return result;
	}

	public async Task CrearPQR(PQRViewModel pqrViewModel, CancellationToken cancellationToken)
	{
		var result = await this.CallService<PQRViewModel, CallCenter.CrearPQRResponse,
			CallCenter.CrearPQRRequest, CallCenter.CrearPQRResponse>
			(pqrViewModel,
			this.callCenterClient.CrearPQRAsync,
			nameof(this.callCenterClient.CrearPQRAsync),
			cancellationToken)
			.ConfigureAwait(false);
	}

	private async Task<TRes> CallService<TRes, TLres>(Func<CancellationToken, Task<TLres>> code, string method, CancellationToken cancellationToken)
	{
		try
		{
			await this.documentaryDbProvider.CreateCallCenterLogAsync(new CallCenterLog
			{
				TypeLog = nameof(TypeLogEnum.HttpRequest),
				LogLevel = nameof(LogLevel.Information),
				MachineName = Environment.MachineName,
				ProjectName = Assembly.GetCallingAssembly().GetName().Name,
				ClassName = this.GetType().Name,
				Method = method,
				DateCreated = DateTime.Now,
			});
			var lresponse = await code(cancellationToken);
			await this.documentaryDbProvider.CreateCallCenterLogAsync(new CallCenterLog
			{
				TypeLog = nameof(TypeLogEnum.HttpResponse),
				LogLevel = nameof(LogLevel.Information),
				MachineName = Environment.MachineName,
				ProjectName = Assembly.GetCallingAssembly().GetName().Name,
				ClassName = this.GetType().Name,
				Method = method,
				Body = lresponse,
				DateCreated = DateTime.Now,
			});
			var response = this.mapper.Map<TLres, TRes>(lresponse);

			return response;
		}
		catch (ApiException apiex)
		{
			switch (apiex.StatusCode)
			{
				case (int)HttpStatusCode.BadRequest:
					throw new DataException(apiex.Response);
				case (int)HttpStatusCode.NotImplemented:
					throw new NotImplementedException(apiex.Response);
				case (int)HttpStatusCode.Conflict:
					throw new InvalidOperationException(apiex.Response);
				case (int)HttpStatusCode.InternalServerError:
					throw new Exception(apiex.Response);
				default:
					throw new Exception(apiex.Response);
			}
		}
	}

	private async Task<TRes> CallService<TReq, TRes, TLreq, TLres>(TReq request, Func<TLreq, CancellationToken, Task<TLres>> code, string method, CancellationToken cancellationToken)
	{
		try
		{
			var lrequest = this.mapper.Map<TReq, TLreq>(request);
			await this.documentaryDbProvider.CreateCallCenterLogAsync(new CallCenterLog
			{
				TypeLog = nameof(TypeLogEnum.HttpRequest),
				LogLevel = nameof(LogLevel.Information),
				MachineName = Environment.MachineName,
				ProjectName = Assembly.GetCallingAssembly().GetName().Name,
				ClassName = this.GetType().Name,
				Method = method,
				Body = lrequest,
				DateCreated = DateTime.Now,
			});
			var lresponse = await code(lrequest, cancellationToken);
			await this.documentaryDbProvider.CreateCallCenterLogAsync(new CallCenterLog
			{
				TypeLog = nameof(TypeLogEnum.HttpResponse),
				LogLevel = nameof(LogLevel.Information),
				MachineName = Environment.MachineName,
				ProjectName = Assembly.GetCallingAssembly().GetName().Name,
				ClassName = this.GetType().Name,
				Method = method,
				Body = lresponse,
				DateCreated = DateTime.Now,
			});
			var response = this.mapper.Map<TLres, TRes>(lresponse);

			return response;
		}
		catch (ApiException apiex)
		{
			switch (apiex.StatusCode)
			{
				case (int)HttpStatusCode.BadRequest:
					throw new DataException(apiex.Response);
				case (int)HttpStatusCode.NotImplemented:
					throw new NotImplementedException(apiex.Response);
				case (int)HttpStatusCode.Conflict:
					throw new InvalidOperationException(apiex.Response);
				case (int)HttpStatusCode.InternalServerError:
					throw new Exception(apiex.Response);
				default:
					throw new Exception(apiex.Response);
			}
		}
	}
}