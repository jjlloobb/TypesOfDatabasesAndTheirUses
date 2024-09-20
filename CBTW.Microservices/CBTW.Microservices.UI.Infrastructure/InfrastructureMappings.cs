using AutoMapper;
using CBTW.Microservices.UI.Domain.CallCenter.ActualizarCliente;
using CBTW.Microservices.UI.Domain.CallCenter.ConsultarCiudades;
using CBTW.Microservices.UI.Domain.CallCenter.ConsultarClientes;
using CBTW.Microservices.UI.Domain.CallCenter.ConsultarTelefonosCC;
using CBTW.Microservices.UI.Domain.CallCenter.ConsultarTiposDeDocumento;
using CBTW.Microservices.UI.Domain.Models;

namespace CBTW.Microservices.UI.Infrastructure;

public class InfrastructureMappings : Profile
{
    public InfrastructureMappings()
    {
        CreateMap<Providers.CallCenter.ClienteResponse, CustomerViewModel>()
            .ForPath(dest =>
                dest.Id,
                opt => opt.MapFrom(src => $"{src.TipoDocumento}@{src.Documento}"))
            .ForPath(dest =>
                dest.IdTipoDocumento,
                opt => opt.MapFrom(src => src.TipoDocumento))
            .ForPath(dest =>
                dest.Documento,
                opt => opt.MapFrom(src => src.Documento))
            .ForPath(dest =>
                dest.NombreCompleto,
                opt => opt.MapFrom(src => src.NombreCompleto))
            .ForPath(dest =>
                dest.Celular,
                opt => opt.MapFrom(src => src.CodigoPaisCelular))
            .ForPath(dest =>
                dest.Celular,
                opt => opt.MapFrom(src => src.Celular))
            .ForPath(dest =>
                dest.IdCiudad,
                opt => opt.MapFrom(src => src.Ciudad))
            .ForPath(dest =>
                dest.FechaNacimiento,
                opt => opt.MapFrom(src => src.FechaNacimiento.Value.DateTime))
            .ForPath(dest =>
                dest.IdTelefonoCC,
                opt => opt.MapFrom(src => src.TelefonoCC));

        CreateMap<CustomerViewModel, Providers.CallCenter.ConsultarClienteRequest>()
            .ForPath(dest =>
                dest.TipoDocumento,
                opt => opt.MapFrom(src => src.IdTipoDocumento))
            .ForPath(dest =>
                dest.Documento,
                opt => opt.MapFrom(src => src.Documento));

        CreateMap<CustomerViewModel, ActualizarClienteValue>()
            .ForPath(dest =>
                dest.TipoDocumento,
                opt => opt.MapFrom(src => src.IdTipoDocumento))
            .ForPath(dest =>
                dest.Documento,
                opt => opt.MapFrom(src => src.Documento))
            .ForPath(dest =>
                dest.NombreCompleto,
                opt => opt.MapFrom(src => src.NombreCompleto))
            .ForPath(dest =>
                dest.CodigoPaisCelular,
                opt => opt.MapFrom(src => src.CodigoPaisCelular))
            .ForPath(dest =>
                dest.Celular,
                opt => opt.MapFrom(src => src.Celular))
            .ForPath(dest =>
                dest.Ciudad,
                opt => opt.MapFrom(src => src.IdCiudad))
            .ForPath(dest =>
                dest.FechaNacimiento,
                opt => opt.MapFrom(src => src.FechaNacimiento))
            .ForPath(dest =>
                dest.TelefonoCC,
                opt => opt.MapFrom(src => src.IdTelefonoCC));

        CreateMap<ActualizarClienteValue, Providers.CallCenter.ActualizarClienteRequest>()
            .ForPath(dest =>
                dest.TipoDocumento,
                opt => opt.MapFrom(src => src.TipoDocumento))
            .ForPath(dest =>
                dest.Documento,
                opt => opt.MapFrom(src => src.Documento))
            .ForPath(dest =>
                dest.NombreCompleto,
                opt => opt.MapFrom(src => src.NombreCompleto))
            .ForPath(dest =>
                dest.CodigoPaisCelular,
                opt => opt.MapFrom(src => src.CodigoPaisCelular))
            .ForPath(dest =>
                dest.Celular,
                opt => opt.MapFrom(src => src.Celular))
            .ForPath(dest =>
                dest.Ciudad,
                opt => opt.MapFrom(src => src.Ciudad))
            .ForPath(dest =>
                dest.FechaNacimiento,
                opt => opt.MapFrom(src => src.FechaNacimiento))
            .ForPath(dest =>
                dest.TelefonoCC,
                opt => opt.MapFrom(src => src.TelefonoCC));

        CreateMap<CustomerViewModel, Providers.CallCenter.CrearClienteRequest>()
            .ForPath(dest =>
                dest.TipoDocumento,
                opt => opt.MapFrom(src => src.IdTipoDocumento))
            .ForPath(dest =>
                dest.Documento,
                opt => opt.MapFrom(src => src.Documento))
            .ForPath(dest =>
                dest.NombreCompleto,
                opt => opt.MapFrom(src => src.NombreCompleto))
            .ForPath(dest =>
                dest.CodigoPaisCelular,
                opt => opt.MapFrom(src => src.CodigoPaisCelular))
            .ForPath(dest =>
                dest.Celular,
                opt => opt.MapFrom(src => src.Celular))
            .ForPath(dest =>
                dest.Ciudad,
                opt => opt.MapFrom(src => src.IdCiudad))
            .ForPath(dest =>
                dest.FechaNacimiento,
                opt => opt.MapFrom(src => src.FechaNacimiento))
            .ForPath(dest =>
                dest.TelefonoCC,
                opt => opt.MapFrom(src => src.IdTelefonoCC));

        CreateMap<Providers.CallCenter.TiposDeDocumentoResponse, TiposDeDocumentoRespuestaValue>()
            .ForPath(dest =>
                dest.Value,
                opt => opt.MapFrom(src => src.Id))
            .ForPath(dest =>
                dest.Text,
                opt => opt.MapFrom(src => $"{src.Name}-{src.Descripcion}"));

        CreateMap<Providers.CallCenter.CiudadResponse, ConsultarCiudadesRespuestaValue>()
            .ForPath(dest =>
                dest.Value,
                opt => opt.MapFrom(src => src.Id))
            .ForPath(dest =>
                dest.Text,
                opt => opt.MapFrom(src => src.Name));

        CreateMap<Providers.CallCenter.TelefonoCCResponse, ConsultarTelefonosCCRespuestaValue>()
            .ForPath(dest =>
                dest.Value,
                opt => opt.MapFrom(src => src.Id))
            .ForPath(dest =>
                dest.Text,
                opt => opt.MapFrom(src => $"{src.CodigoPaisCelular} {src.Celular}"));

        CreateMap<Providers.CallCenter.ConsultarClienteResponse, CustomerViewModel>()
            .ForPath(dest =>
                dest.IdCustomer,
                opt => opt.MapFrom(src => src.Id))
            .ForPath(dest =>
                dest.Id,
                opt => opt.MapFrom(src => $"{src.TipoDocumento}@{src.Documento}"))
            .ForPath(dest =>
                dest.IdTipoDocumento,
                opt => opt.MapFrom(src => src.TipoDocumento))
            .ForPath(dest =>
                dest.Documento,
                opt => opt.MapFrom(src => src.Documento))
            .ForPath(dest =>
                dest.NombreCompleto,
                opt => opt.MapFrom(src => src.NombreCompleto))
            .ForPath(dest =>
                dest.CodigoPaisCelular,
                opt => opt.MapFrom(src => src.CodigoPaisCelular))
            .ForPath(dest =>
                dest.Celular,
                opt => opt.MapFrom(src => src.Celular))
            .ForPath(dest =>
                dest.IdCiudad,
                opt => opt.MapFrom(src => src.Ciudad))
            .ForPath(dest =>
                dest.FechaNacimiento,
                opt => opt.MapFrom(src => src.FechaNacimiento.Value.DateTime))
            .ForPath(dest =>
                dest.IdTelefonoCC,
                opt => opt.MapFrom(src => src.TelefonoCC));

        CreateMap<Providers.CallCenter.PQRResponse, PQRViewModel>()
            .ForPath(dest =>
                dest.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForPath(dest =>
                dest.IdCustomer,
                opt => opt.MapFrom(src => src.IdCustomer))
            .ForPath(dest =>
                dest.Asunto,
                opt => opt.MapFrom(src => src.Subject))
            .ForPath(dest =>
                dest.Descripcion,
                opt => opt.MapFrom(src => src.Description));

        CreateMap<PQRViewModel, Providers.CallCenter.CrearPQRRequest>()
            .ForPath(dest =>
                dest.Customer,
                opt => opt.MapFrom(src => src.Customer))
            .ForPath(dest =>
                dest.Asunto,
                opt => opt.MapFrom(src => src.Asunto))
            .ForPath(dest =>
                dest.Descripcion,
                opt => opt.MapFrom(src => src.Descripcion));        

        CreateMap<Providers.CallCenter.ConsultarClienteResponse, ConsultarClientesRespuestaValue>()
            .ForPath(dest =>
                dest.Value,
                opt => opt.MapFrom(src => $"{src.TipoDocumento}@{src.Documento}"))
            .ForPath(dest =>
                dest.Text,
                opt => opt.MapFrom(src => $"{src.Documento}--{src.NombreCompleto}"));

		CreateMap<CustomerViewModel, ConsultarClientesRespuestaValue>()
			.ForPath(dest =>
				dest.Value,
				opt => opt.MapFrom(src => src.Id))
			.ForPath(dest =>
				dest.Text,
				opt => opt.MapFrom(src => $"{src.Documento}--{src.NombreCompleto}"));

		CreateMap<Providers.CallCenter.PQRResponse, Providers.CallCenter.ConsultarClienteRequest>()
            .ForPath(dest =>
                dest.Id,
                opt => opt.MapFrom(src => src.IdCustomer));

		CreateMap<PQRViewModel, Providers.CallCenter.ConsultarPQRRequest>()
			.ForPath(dest =>
				dest.Id,
				opt => opt.MapFrom(src => src.Id))
			.ForPath(dest =>
				dest.IdCustomer,
				opt => opt.MapFrom(src => src.IdCustomer));

		CreateMap<Providers.CallCenter.ConsultarPQRResponse, PQRViewModel>()
			.ForPath(dest =>
				dest.Id,
				opt => opt.MapFrom(src => src.Id))
            .ForPath(dest =>
                dest.IdCustomer,
                opt => opt.MapFrom(src => src.IdCustomer))
            .ForPath(dest =>
				dest.Asunto,
				opt => opt.MapFrom(src => src.Subject))
			.ForPath(dest =>
				dest.Descripcion,
				opt => opt.MapFrom(src => src.Description));

		CreateMap<Providers.CallCenter.ConsultarPQRResponse, Providers.CallCenter.PQRResponse>()
			.ForPath(dest =>
				dest.Id,
				opt => opt.MapFrom(src => src.Id))
            .ForPath(dest =>
                dest.IdCustomer,
                opt => opt.MapFrom(src => src.IdCustomer))
            .ForPath(dest =>
				dest.Subject,
				opt => opt.MapFrom(src => src.Subject))
			.ForPath(dest =>
				dest.Description,
				opt => opt.MapFrom(src => src.Description));
	}
}