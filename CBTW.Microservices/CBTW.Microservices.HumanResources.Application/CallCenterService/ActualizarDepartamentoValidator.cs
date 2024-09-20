using CBTW.Microservices.HumanResources.Service.Requests;
using FluentValidation;

namespace CBTW.Microservices.HumanResources.Application.CallCenterService;

public class ActualizarDepartamentoValidator : AbstractValidator<ActualizarDepartamentoRequest>
{
    public ActualizarDepartamentoValidator(CancellationToken cancellationToken = default(CancellationToken))
    {
		RuleFor(j => j.Abreviacion)
			.NotNullOrEmpty()
			.NotStartWithWhiteSpace()
			.NotEndWithWhiteSpace()
			.NotLength(1, 5);

		RuleFor(j => j.NombreCompleto)
			.NotNullOrEmpty()
			.NotStartWithWhiteSpace()
			.NotEndWithWhiteSpace()
			.NotLength(1, 200);

		RuleFor(j => j.Descripcion)
			.NotNullOrEmpty()
			.NotStartWithWhiteSpace()
			.NotEndWithWhiteSpace()
			.NotLength(1, 4000);

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
	}
}