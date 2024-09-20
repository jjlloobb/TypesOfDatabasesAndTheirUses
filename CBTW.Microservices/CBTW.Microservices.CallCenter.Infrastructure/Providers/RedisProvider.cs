using CBTW.Microservices.CallCenter.Application.Providers;
using CBTW.Microservicios.CallCenter.Aplicacion.Configurations;
using CBTW.Microservicios.CallCenter.Aplicacion.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace CBTW.Microservices.CallCenter.Infrastructure.Providers;

/// <summary>
/// Implementación de la interfaz IMemoryProvider
/// </summary>
public class RedisProvider : IMemoryProvider
{
	#region Private Fields

	/// <summary>
	/// Proveedor de cache distribuida
	/// </summary>
	private readonly IDistributedCache distributedCache;

	/// <summary>
	/// Proveedor de configuración para redis
	/// </summary>
	private readonly IOptions<RedisOptions> options;

	#endregion

	#region Constructor

	/// <summary>
	/// Constructor de la clase
	/// </summary>
	/// <param name="distributedCache">Inyección de cache distribuida</param>
	/// <param name="options">Inyección de options</param>
	public RedisProvider(IDistributedCache distributedCache,
		IOptions<RedisOptions> options)
	{
		this.distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
		this.options = options ?? throw new ArgumentNullException(nameof(options));
	}

	#endregion

	#region Implementations

	/// <summary>
	/// Método para obtener una key
	/// </summary>
	/// <param name="key">Objeto de tipo key</param>
	/// <param name="cancellationToken">Token de cancelación</param>
	/// <returns>Resultado de la operación</returns>
	public async Task<T> GetCacheValueAsync<T>(string key,
		CancellationToken cancellationToken)
		where T : class
	{
		var result = string.Empty;

		result = await this.distributedCache.GetStringAsync(
			key,
			cancellationToken)
			.ConfigureAwait(false);

		if (string.IsNullOrEmpty(result))
			return null;

		return result.JsonToObject<T>();
	}

	/// <summary>
	/// Método para obtener una key
	/// </summary>
	/// <param name="key">Objeto de tipo key</param>
	/// <param name="cancellationToken">Token de cancelación</param>
	/// <returns>Resultado de la operación</returns>
	public async Task<List<T>> GetCacheValuesAsync<T>(string key,
		CancellationToken cancellationToken)
		where T : class
	{
		var result = string.Empty;

		result = await this.distributedCache.GetStringAsync(
			key,
			cancellationToken)
			.ConfigureAwait(false);

		if (string.IsNullOrEmpty(result))
			return null;

		return result.JsonToObjectList<T>();
	}

	/// <summary>
	/// Método para obtener una key
	/// </summary>
	/// <param name="key">Objeto de tipo key</param>
	/// <param name="predicate">Obejto de tipo delegado</param>
	/// <param name="cancellationToken">Token de cancelación</param>
	/// <returns>Resultado de la operación</returns>
	public async Task<T> GetCacheValueAsync<T>(string key,
		Func<Task<T>> predicate,
		CancellationToken cancellationToken)
		where T : class
	{
		var result = string.Empty;
		T lresult = null;


		result = await this.distributedCache.GetStringAsync(
			key,
			cancellationToken)
			.ConfigureAwait(false);

		if (!string.IsNullOrEmpty(result))
			return result.JsonToObject<T>();

		lresult = await predicate().ConfigureAwait(false);

		var distributedCacheOptions = new DistributedCacheEntryOptions
		{
			AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(3650),
			SlidingExpiration = TimeSpan.FromDays(3650)
		};

		await this.distributedCache.SetStringAsync(
			key,
			lresult.ToJson(),
			distributedCacheOptions,
			cancellationToken).ConfigureAwait(false);

		return lresult;
	}

	/// <summary>
	/// Método para obtener una key
	/// </summary>
	/// <param name="key">Objeto de tipo key</param>
	/// <param name="predicate">Obejto de tipo delegado</param>
	/// <param name="cancellationToken">Token de cancelación</param>
	/// <param name="distributedCacheExpiration">Objeto de tipo distributed cache expiration</param>
	/// <returns>Resultado de la operación</returns>
	public async Task<T> GetCacheValueAsync<T>(string key,
		Func<Task<T>> predicate,
		CancellationToken cancellationToken,
		bool distributedCacheExpiration = false)
		where T : class
	{
		var result = string.Empty;
		T lresult = null;
		DistributedCacheEntryOptions distributedCacheOptions = null;

		result = await this.distributedCache.GetStringAsync(
			key,
			cancellationToken)
			.ConfigureAwait(false);

		if (!string.IsNullOrEmpty(result))
			return result.JsonToObject<T>();

		lresult = await predicate().ConfigureAwait(false);

		distributedCacheOptions = new DistributedCacheEntryOptions();
		if (!distributedCacheExpiration)
		{
			distributedCacheOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(3650);
			distributedCacheOptions.SlidingExpiration = TimeSpan.FromDays(3650);
		}
		else
		{
			distributedCacheOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(this.options.Value.AbsoluteExpirationRelativeToNow);
			distributedCacheOptions.SlidingExpiration = TimeSpan.FromHours(this.options.Value.SlidingExpiration);
		}

		await this.distributedCache.SetStringAsync(
			key,
			lresult.ToJson(),
			distributedCacheOptions,
			cancellationToken).ConfigureAwait(false);

		return lresult;
	}

	/// <summary>
	/// Método para establecer una key
	/// </summary>
	/// <param name="key">Objeto de tipo key</param>
	/// <param name="value">Objeto de tipo value</param>
	/// <param name="cancellationToken">Token de cancelación</param>
	/// <returns>Resultado de la operación</returns>
	public async Task SetCacheValueAsync<T>(string key,
		T value,
		CancellationToken cancellationToken)
		where T : class
	{
		DistributedCacheEntryOptions distributedCacheOptions = null;

		distributedCacheOptions = new DistributedCacheEntryOptions
		{
			AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(3650),
			SlidingExpiration = TimeSpan.FromDays(3650)
		};

		await this.distributedCache.SetStringAsync(
			key,
			value.ToJson(),
			distributedCacheOptions,
			cancellationToken).ConfigureAwait(false);
	}	

	/// <summary>
	/// Método para establecer una key
	/// </summary>
	/// <param name="key">Objeto de tipo key</param>
	/// <param name="value">Objeto de tipo value</param>        
	/// <param name="cancellationToken">Token de cancelación</param>
	/// <param name="distributedCacheExpiration">Objeto de tipo distributed cache expiration</param>
	/// <returns>Resultado de la operación</returns>
	public async Task SetCacheValueAsync<T>(string key,
		T value,
		CancellationToken cancellationToken,
		bool distributedCacheExpiration = false)
		where T : class
	{
		DistributedCacheEntryOptions distributedCacheOptions = null;

		distributedCacheOptions = new DistributedCacheEntryOptions();
		if (!distributedCacheExpiration)
		{
			distributedCacheOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(3650);
			distributedCacheOptions.SlidingExpiration = TimeSpan.FromDays(3650);
		}
		else
		{
			distributedCacheOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(this.options.Value.AbsoluteExpirationRelativeToNow);
			distributedCacheOptions.SlidingExpiration = TimeSpan.FromHours(this.options.Value.SlidingExpiration);
		}

		await this.distributedCache.SetStringAsync(
			key,
			value.ToJson(),
			distributedCacheOptions,
			cancellationToken).ConfigureAwait(false);
	}

	/// <summary>
	/// Método para establecer una key
	/// </summary>
	/// <param name="key">Objeto de tipo key</param>
	/// <param name="value">Objeto de tipo value</param>        
	/// <param name="cancellationToken">Token de cancelación</param>
	/// <param name="distributedCacheExpiration">Objeto de tipo distributed cache expiration</param>
	/// <returns>Resultado de la operación</returns>
	public async Task SetCacheValuesAsync<T>(string key,
		List<T> value,
		CancellationToken cancellationToken,
		bool distributedCacheExpiration = false)
		where T : class
	{
		DistributedCacheEntryOptions distributedCacheOptions = null;

		distributedCacheOptions = new DistributedCacheEntryOptions();
		if (!distributedCacheExpiration)
		{
			distributedCacheOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(3650);
			distributedCacheOptions.SlidingExpiration = TimeSpan.FromDays(3650);
		}
		else
		{
			distributedCacheOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(this.options.Value.AbsoluteExpirationRelativeToNow);
			distributedCacheOptions.SlidingExpiration = TimeSpan.FromHours(this.options.Value.SlidingExpiration);
		}

		await this.distributedCache.SetStringAsync(
			key,
			value.ToJson(),
			distributedCacheOptions,
			cancellationToken).ConfigureAwait(false);
	}

	/// <summary>
	/// Método para remover una key
	/// </summary>
	/// <param name="key">Objeto de tipo key</param>
	/// <param name="cancellationToken">Token de cancelación</param>
	/// <returns>Resultado de la operación</returns>
	public async Task RemoveCacheValueAsync(string key,
		CancellationToken cancellationToken)
	{
		await this.distributedCache.RemoveAsync(
			key,
			cancellationToken)
			.ConfigureAwait(false);
	}

	#endregion
}