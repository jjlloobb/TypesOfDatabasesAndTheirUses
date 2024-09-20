using CBTW.Microservices.HumanResources.Service.Requests;
using FluentValidation;

namespace CBTW.Microservices.HumanResources.Application.CallCenterService;

public class ConsultarDepartamentoValidator : AbstractValidator<ConsultarDepartamentoRequest>
{
    public ConsultarDepartamentoValidator(CancellationToken cancellationToken = default(CancellationToken))
    {
		WhenAsync(async (j, cancellationToken) => await Task.Run(() => j.Abreviacion != null), () =>
		{
			RuleFor(w => w.Abreviacion)
				.NotNullOrEmpty()
				.NotStartWithWhiteSpace()
				.NotEndWithWhiteSpace()
				.NotLength(1, 5);
		});
	}
}