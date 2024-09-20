using CBTW.Microservices.HumanResources.Service.Requests;
using FluentValidation;

namespace CBTW.Microservices.HumanResources.Application.CallCenterService;

public class ConsultarEmpleadoValidator : AbstractValidator<ConsultarEmpleadoRequest>
{
    public ConsultarEmpleadoValidator(CancellationToken cancellationToken = default(CancellationToken))
    {
		WhenAsync(async (j, cancellationToken) => await Task.Run(() => j.TipoDocumento != null), () =>
		{
			RuleFor(w => w.TipoDocumento)
				.NotNullOrEmpty()
				.NotStartWithWhiteSpace()
				.NotEndWithWhiteSpace()
				.NotLength(1, 5);
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