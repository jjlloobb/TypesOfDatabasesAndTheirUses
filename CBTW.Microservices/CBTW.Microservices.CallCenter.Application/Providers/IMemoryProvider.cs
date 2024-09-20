namespace CBTW.Microservices.CallCenter.Application.Providers;

/// <summary>
/// Interfaz in-memory data structured store
/// </summary>
public interface IMemoryProvider
{
	/// <summary>
	/// Método para obtener una key
	/// </summary>
	/// <param name="key">Objeto de tipo key</param>
	/// <param name="cancellationToken">Token de cancelación</param>
	/// <returns>Resultado de la operación</returns>
	public Task<T> GetCacheValueAsync<T>(string key, CancellationToken cancellationToken) where T : class;

	/// <summary>
	/// Método para obtener una key
	/// </summary>
	/// <param name="key">Objeto de tipo key</param>
	/// <param name="cancellationToken">Token de cancelación</param>
	/// <returns>Resultado de la operación</returns>
	public Task<List<T>> GetCacheValuesAsync<T>(string key, CancellationToken cancellationToken) where T : class;

	/// <summary>
	/// Método para obtener una key
	/// </summary>
	/// <param name="key">Objeto de tipo key</param>
	/// <param name="predicate">Obejto de tipo delegado</param>
	/// <param name="cancellationToken">Token de cancelación</param>
	/// <returns>Resultado de la operación</returns>
	public Task<T> GetCacheValueAsync<T>(string key, Func<Task<T>> predicate, CancellationToken cancellationToken) where T : class;

	/// <summary>
	/// Método para obtener una key
	/// </summary>
	/// <param name="key">Objeto de tipo key</param>
	/// <param name="predicate">Obejto de tipo delegado</param>
	/// <param name="cancellationToken">Token de cancelación</param>
	/// <param name="distributedCacheExpiration">Objeto de tipo distributed cache expiration</param>
	/// <returns>Resultado de la operación</returns>
	public Task<T> GetCacheValueAsync<T>(string key, Func<Task<T>> predicate, CancellationToken cancellationToken, bool distributedCacheExpiration = false) where T : class;

	/// <summary>
	/// Método para establecer una key
	/// </summary>
	/// <param name="key">Objeto de tipo key</param>
	/// <param name="value">Objeto de tipo value</param>
	/// <param name="cancellationToken">Token de cancelación</param>
	/// <returns>Resultado de la operación</returns>
	public Task SetCacheValueAsync<T>(string key, T value, CancellationToken cancellationToken) where T : class;

	/// <summary>
	/// Método para establecer una key
	/// </summary>
	/// <param name="key">Objeto de tipo key</param>
	/// <param name="value">Objeto de tipo value</param>
	/// <param name="distributedCacheExpiration">Objeto de tipo distributed cache expiration</param>
	/// <param name="cancellationToken">Token de cancelación</param>
	/// <returns>Resultado de la operación</returns>
	public Task SetCacheValueAsync<T>(string key, T value, CancellationToken cancellationToken, bool distributedCacheExpiration = false) where T : class;

	/// <summary>
	/// Método para establecer una key
	/// </summary>
	/// <param name="key">Objeto de tipo key</param>
	/// <param name="value">Objeto de tipo value</param>
	/// <param name="distributedCacheExpiration">Objeto de tipo distributed cache expiration</param>
	/// <param name="cancellationToken">Token de cancelación</param>
	/// <returns>Resultado de la operación</returns>
	public Task SetCacheValuesAsync<T>(string key, List<T> value, CancellationToken cancellationToken, bool distributedCacheExpiration = false) where T : class;

	/// <summary>
	/// Método para remover una key
	/// </summary>
	/// <param name="key">Objeto de tipo key</param>
	/// <param name="cancellationToken">Token de cancelación</param>
	/// <returns>Resultado de la operación</returns>
	public Task RemoveCacheValueAsync(string key, CancellationToken cancellationToken);
}