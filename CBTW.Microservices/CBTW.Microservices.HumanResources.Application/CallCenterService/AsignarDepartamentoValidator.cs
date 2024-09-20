using CBTW.Microservices.HumanResources.Service.Requests;
using FluentValidation;

namespace CBTW.Microservices.HumanResources.Application.CallCenterService;

public class AsignarDepartamentoValidator : AbstractValidator<AsignarDepartamentoRequest>
{
	public AsignarDepartamentoValidator(CancellationToken cancellationToken = default(CancellationToken))
	{
		RuleFor(w => w.TipoDocumento)
			.NotNullOrEmpty()
			.NotStartWithWhiteSpace()
			.NotEndWithWhiteSpace()
			.NotLength(1, 5);

		RuleFor(w => w.Documento)
			.NotNullOrEmpty()
			.NotStartWithWhiteSpace()
			.NotEndWithWhiteSpace()
			.NotLength(1, 20);

		RuleFor(j => j.Abreviacion)
			.NotNullOrEmpty()
			.NotStartWithWhiteSpace()
			.NotEndWithWhiteSpace()
			.NotLength(1, 5);		
	}
}