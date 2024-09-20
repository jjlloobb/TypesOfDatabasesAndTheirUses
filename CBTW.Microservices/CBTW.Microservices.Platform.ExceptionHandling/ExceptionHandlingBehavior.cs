using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CBTW.Microservices.Platform.ExceptionHandling;

public class ExceptionHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger logger;

    public ExceptionHandlingBehavior(ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            var response = await next();

            return response;
        }
        catch (FluentValidation.ValidationException vex)
        {
            this.logger.LogError(vex, "ValidationException");

            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);

            throw new PlatformResponseException(response);
        }
        catch (NotImplementedException niex)
        {
            this.logger.LogError(niex, "NotImplementedException");

            var response = new HttpResponseMessage(HttpStatusCode.NotImplemented);

            throw new PlatformResponseException(response);
        }
        catch (InvalidOperationException ioex)
        {
            this.logger.LogError(ioex, "InvalidOperationException");

            var response = new HttpResponseMessage(HttpStatusCode.Conflict);

            throw new PlatformResponseException(response);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Exception");

            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);

            throw new PlatformResponseException(response);
        }
    }
}