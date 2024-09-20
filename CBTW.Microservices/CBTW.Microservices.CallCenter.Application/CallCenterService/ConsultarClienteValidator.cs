using CBTW.Microservices.CallCenter.Application.Providers;
using CBTW.Microservices.CallCenter.Service.Requests;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CBTW.Microservices.CallCenter.Application.CallCenterService;

public class ConsultarClienteValidator : AbstractValidator<ConsultarClienteRequest>
{
    private readonly IUnitOfWork unitOfWork;

    public ConsultarClienteValidator(IUnitOfWork unitOfWork,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        WhenAsync(async (j, cancellationToken) => await Task.Run(() => j.Id != null), () =>
        {
            RuleFor(w => w.Id)
                .NotGreaterThan(0);
        });

        WhenAsync(async (j, cancellationToken) => await Task.Run(() => j.TipoDocumento != null), () =>
        {
            RuleFor(w => w.TipoDocumento)
                .NotGreaterThan(0);

            RuleFor(w => w)
            .CustomAsync(async (y, context, cancellationToken) =>
            {
                var documentType = await this.unitOfWork.DocumentTypes
                                                    .FindBy(x => x.Id == y.TipoDocumento)
                                                    .FirstOrDefaultAsync(cancellationToken)
                                                    .ConfigureAwait(false);

                if (documentType == null) context.AddFailure("Propiedad 'TipoDocumento' no encontrada.");
            });
        });

        WhenAsync(async (j, cancellationToken) => await Task.Run(() => j.Documento != null), () =>
        {
            RuleFor(w => w.Documento)
                .NotNullOrEmpty()
                .NotStartWithWhiteSpace()
                .NotEndWithWhiteSpace()
                .NotLength(1, 20);
        });
    }
}
