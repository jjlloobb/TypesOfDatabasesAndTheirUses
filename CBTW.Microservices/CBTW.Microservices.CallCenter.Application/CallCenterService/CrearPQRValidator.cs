using CBTW.Microservices.CallCenter.Application.Providers;
using CBTW.Microservices.CallCenter.Service.Requests;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CBTW.Microservices.CallCenter.Application.CallCenterService;

public class CrearPQRValidator : AbstractValidator<CrearPQRRequest>
{
    private readonly IUnitOfWork unitOfWork;

    public CrearPQRValidator(IUnitOfWork unitOfWork,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        RuleFor(j => j.Customer)
            .NotNullOrEmpty()
            .NotStartWithWhiteSpace()
            .NotEndWithWhiteSpace();

        RuleFor(j => j.Asunto)
            .NotNullOrEmpty()
            .NotStartWithWhiteSpace()
            .NotEndWithWhiteSpace()
            .NotLength(1, 200);

        RuleFor(j => j.Descripcion)
            .NotNullOrEmpty()
            .NotStartWithWhiteSpace()
            .NotEndWithWhiteSpace()
            .NotLength(1, 4000);

        RuleFor(j => j)
            .CustomAsync(async (j, context, cancellationToken) =>
            {
                var tipoDeDocumento = int.Parse(j.Customer.Split('@')[0] ?? "0");
                var documento = j.Customer.Split('@')[1] ?? string.Empty;
                var customer = await this.unitOfWork.Customers
                                                    .FindBy(x => x.IdDocumentType == tipoDeDocumento
                                                            && x.Document == documento)
                                                    .FirstOrDefaultAsync(cancellationToken)
                                                    .ConfigureAwait(false);

                if (customer == null) context.AddFailure("Propiedad 'Customer' no encontrada.");
            });
    }
}
