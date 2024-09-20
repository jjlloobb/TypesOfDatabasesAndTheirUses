using CBTW.Microservices.CallCenter.Application.Providers;
using CBTW.Microservices.CallCenter.Service.Requests;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CBTW.Microservices.CallCenter.Application.CallCenterService;

public class ConsultarPQRValidator : AbstractValidator<ConsultarPQRRequest>
{
    private readonly IUnitOfWork unitOfWork;

    public ConsultarPQRValidator(IUnitOfWork unitOfWork,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        WhenAsync(async (j, cancellationToken) => await Task.Run(() => j.Id != null), () =>
        {
            RuleFor(w => w.Id)
                .NotGreaterThan(0);
        });

        WhenAsync(async (j, cancellationToken) => await Task.Run(() => j.IdCustomer != null && j.IdCustomer != 0), () =>
        {
            RuleFor(w => w.IdCustomer)
                .NotGreaterThan(0);

            RuleFor(w => w)
            .CustomAsync(async (y, context, cancellationToken) =>
            {
                var customer = await this.unitOfWork.Customers
                                                    .FindBy(x => x.Id == y.IdCustomer)
                                                    .FirstOrDefaultAsync(cancellationToken)
                                                    .ConfigureAwait(false);

                if (customer == null) context.AddFailure("Propiedad 'IdCustomer' no encontrada.");
            });
        });
    }
}
