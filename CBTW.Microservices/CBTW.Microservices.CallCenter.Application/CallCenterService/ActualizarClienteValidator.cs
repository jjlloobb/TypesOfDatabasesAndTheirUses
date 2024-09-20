using CBTW.Microservices.CallCenter.Application.Providers;
using CBTW.Microservices.CallCenter.Service.Requests;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CBTW.Microservices.CallCenter.Application.CallCenterService;

public class ActualizarClienteValidator : AbstractValidator<ActualizarClienteRequest>
{
    private readonly IUnitOfWork unitOfWork;

    public ActualizarClienteValidator(IUnitOfWork unitOfWork,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        RuleFor(j => j.TipoDocumento)
            .NotGreaterThan(0);

        RuleFor(j => j.Documento)
            .NotNullOrEmpty()
            .NotStartWithWhiteSpace()
            .NotEndWithWhiteSpace()
            .NotLength(1, 20);

        RuleFor(j => j.NombreCompleto)
            .NotNullOrEmpty()
            .NotStartWithWhiteSpace()
            .NotEndWithWhiteSpace()
            .NotLength(1, 200);

        RuleFor(j => j.CodigoPaisCelular)
            .NotNullOrEmpty()
            .NotStartWithWhiteSpace()
            .NotEndWithWhiteSpace()
            .NotLength(1, 5);

        RuleFor(j => j.Celular)
            .NotNullOrEmpty()
            .NotStartWithWhiteSpace()
            .NotEndWithWhiteSpace()
            .NotLength(1, 15);

        RuleFor(j => j.Ciudad)
            .NotGreaterThan(0);

        RuleFor(j => j.FechaNacimiento)
            .NotLessThanOrEqualTo(DateTime.Now);

        RuleFor(j => j.TelefonoCC)
            .NotGreaterThan(0);

        RuleFor(j => j)
            .CustomAsync(async (j, context, cancellationToken) =>
            {
                var documentType = await this.unitOfWork.DocumentTypes
                                                    .FindBy(x => x.Id == j.TipoDocumento)
                                                    .FirstOrDefaultAsync(cancellationToken)
                                                    .ConfigureAwait(false);

                if (documentType == null) context.AddFailure("Propiedad 'TipoDocumento' no encontrada.");

                var city = await this.unitOfWork.Cities
                                            .FindBy(x => x.Id == j.Ciudad)
                                            .FirstOrDefaultAsync(cancellationToken)
                                            .ConfigureAwait(false);

                if (city == null) context.AddFailure("Propiedad 'Ciudad' no encontrada.");

                var phoneCC = await this.unitOfWork.Phones
                                                .FindBy(x => x.Id == j.TelefonoCC)
                                                .FirstOrDefaultAsync(cancellationToken)
                                                .ConfigureAwait(false);

                if (phoneCC == null) context.AddFailure("Propiedad 'TelefonoCC' no encontrada.");
            });
    }
}
