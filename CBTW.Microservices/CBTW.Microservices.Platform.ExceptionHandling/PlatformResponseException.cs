using System.Net;

namespace CBTW.Microservices.Platform.ExceptionHandling;

public class PlatformResponseException : Exception
{
    public PlatformResponseException(HttpStatusCode statusCode)
        : this(new HttpResponseMessage(statusCode))
    {
    }

    public PlatformResponseException(HttpResponseMessage response)
    {
        Response = response ?? throw new ArgumentNullException(nameof(response));
    }

    public HttpResponseMessage Response { get; private set; }
}
