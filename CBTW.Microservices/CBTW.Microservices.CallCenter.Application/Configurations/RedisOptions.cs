namespace CBTW.Microservicios.CallCenter.Aplicacion.Configurations;

/// <summary>
/// Clase options redis
/// </summary>
public class RedisOptions
{
    /// <summary>
    /// Tiempo en horas de la expiración absoluta de una key
    /// </summary>
    public double AbsoluteExpirationRelativeToNow { get; set; }

    /// <summary>
    /// Tiempo en horas de la expiración deslizante de una key
    /// </summary>
    public double SlidingExpiration { get; set; }
}
