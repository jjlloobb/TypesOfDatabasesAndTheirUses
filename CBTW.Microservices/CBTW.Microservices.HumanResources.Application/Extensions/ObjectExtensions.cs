using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CBTW.Microservicios.HumanResources.Aplicacion.Extensions;

/// <summary>
/// Clase extensiones para objetos
/// </summary>
public static class ObjectExtensions
{
	#region Private Fields

	private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
	{
		ContractResolver = new CamelCasePropertyNamesContractResolver(),
		NullValueHandling = NullValueHandling.Ignore,
		ReferenceLoopHandling = ReferenceLoopHandling.Ignore
	};

	#endregion

	#region Extensions

	/// <summary>
	/// Método serializar objeto a json
	/// </summary>
	public static string ToJson<T>(this T obj)
	{
		return JsonConvert.SerializeObject(obj, jsonSerializerSettings);
	}

	/// <summary>
	/// Método serializar objeto a json
	/// </summary>
	public static string ToJson<T>(this List<T> obj)
	{
		return JsonConvert.SerializeObject(obj, jsonSerializerSettings);
	}

	/// <summary>
	/// Método deserializar json a objeto
	/// </summary>
	public static T JsonToObject<T>(this string json)
	{
		var value = json ?? throw new ArgumentNullException($"JsonToObject 'json'");
		try
		{
			return JsonConvert.DeserializeObject<T>(value);
		}
		catch (Exception ex)
		{
			throw new FormatException($"Unable to deserialize 'value' {value}", innerException: ex);
		}
	}

	/// <summary>
	/// Método deserializar json a lista de objetos
	/// </summary>
	public static List<T> JsonToObjectList<T>(this string json)
	{
		var value = json ?? throw new ArgumentNullException($"JsonToObject 'json'");
		try
		{
			return JsonConvert.DeserializeObject<List<T>>(value);
		}
		catch (Exception ex)
		{
			throw new FormatException($"Unable to deserialize 'value' {value}", innerException: ex);
		}
	}

	#endregion
}