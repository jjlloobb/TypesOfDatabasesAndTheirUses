using CBTW.Microservices.HumanResources.Service.Requests;
using FluentValidation;

namespace CBTW.Microservices.HumanResources.Application.CallCenterService;

public class CrearEmpleadoValidator : AbstractValidator<CrearEmpleadoRequest>
{
    public CrearEmpleadoValidator(CancellationToken cancellationToken = default(CancellationToken))
    {
        RuleFor(j => j.TipoDocumento)
			.NotNullOrEmpty()
			.NotStartWithWhiteSpace()
			.NotEndWithWhiteSpace()
			.NotLength(1, 5);

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

        RuleFor(j => j.FechaNacimiento)
            .NotLessThanOrEqualTo(DateTime.Now);
    }
}