using AutoMapper;
using CBTW.Microservices.CallCenter.Domain.CallCenter;
using CBTW.Microservices.CallCenter.Service.Requests;
using CBTW.Microservices.CallCenter.Service.Responses;

namespace CBTW.Microservices.CallCenter.Application;

public class ApplicationMappings : Profile
{
    public ApplicationMappings()
    {
        CreateMap<CrearClienteRequest, CustomerEntity>()
            .ForPath(dest =>
                dest.IdDocumentType,
                opt => opt.MapFrom(src => src.TipoDocumento))
            .ForPath(dest =>
                dest.Document,
                opt => opt.MapFrom(src => src.Documento))
            .ForPath(dest =>
                dest.FullName,
                opt => opt.MapFrom(src => src.NombreCompleto))
            .ForPath(dest =>
                dest.CountryCode,
                opt => opt.MapFrom(src => src.CodigoPaisCelular))
            .ForPath(dest =>
                dest.PhoneNumber,
                opt => opt.MapFrom(src => src.Celular))
            .ForPath(dest =>
                dest.IdCity,
                opt => opt.MapFrom(src => src.Ciudad))
            .ForPath(dest =>
                dest.DateOfBirth,
                opt => opt.MapFrom(src => src.FechaNacimiento))
            .ForPath(dest =>
                dest.IdPhoneCC,
                opt => opt.MapFrom(src => src.TelefonoCC))
            .ForPath(dest =>
                dest.CreateDate,
                opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<CustomerEntity, ConsultarClienteResponse>()
			.ForPath(dest =>
				dest.Id,
				opt => opt.MapFrom(src => src.Id))
			.ForPath(dest =>
                dest.TipoDocumento,
                opt => opt.MapFrom(src => src.IdDocumentType))
            .ForPath(dest =>
                dest.Documento,
                opt => opt.MapFrom(src => src.Document))
            .ForPath(dest =>
                dest.NombreCompleto,
                opt => opt.MapFrom(src => src.FullName))
            .ForPath(dest =>
                dest.CodigoPaisCelular,
                opt => opt.MapFrom(src => src.CountryCode))
            .ForPath(dest =>
                dest.Celular,
                opt => opt.MapFrom(src => src.PhoneNumber))
            .ForPath(dest =>
                dest.Ciudad,
                opt => opt.MapFrom(src => src.IdCity))
            .ForPath(dest =>
                dest.FechaNacimiento,
                opt => opt.MapFrom(src => src.DateOfBirth))
            .ForPath(dest =>
                dest.TelefonoCC,
                opt => opt.MapFrom(src => src.IdPhoneCC));

        CreateMap<CustomerEntity, ClienteResponse>()
            .ForPath(dest =>
                dest.TipoDocumento,
                opt => opt.MapFrom(src => src.IdDocumentType))
            .ForPath(dest =>
                dest.Documento,
                opt => opt.MapFrom(src => src.Document))
            .ForPath(dest =>
                dest.NombreCompleto,
                opt => opt.MapFrom(src => src.FullName))
            .ForPath(dest =>
                dest.CodigoPaisCelular,
                opt => opt.MapFrom(src => src.CountryCode))
            .ForPath(dest =>
                dest.Celular,
                opt => opt.MapFrom(src => src.PhoneNumber))
            .ForPath(dest =>
                dest.Ciudad,
                opt => opt.MapFrom(src => src.IdCity))
            .ForPath(dest =>
                dest.FechaNacimiento,
                opt => opt.MapFrom(src => src.DateOfBirth))
            .ForPath(dest =>
                dest.TelefonoCC,
                opt => opt.MapFrom(src => src.IdPhoneCC));

        CreateMap<DocumentTypeEntity, TiposDeDocumentoResponse>()
            .ForPath(dest =>
                dest.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForPath(dest =>
                dest.Name,
                opt => opt.MapFrom(src => src.Name))
            .ForPath(dest =>
                dest.Descripcion,
                opt => opt.MapFrom(src => src.Description));

        CreateMap<CityEntity, CiudadResponse>()
            .ForPath(dest =>
                dest.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForPath(dest =>
                dest.Name,
                opt => opt.MapFrom(src => src.Name));

        CreateMap<PhoneCCEntity, TelefonoCCResponse>()
            .ForPath(dest =>
                dest.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForPath(dest =>
                dest.CodigoPaisCelular,
                opt => opt.MapFrom(src => src.CountryCode))
            .ForPath(dest =>
                dest.Celular,
                opt => opt.MapFrom(src => src.PhoneNumber));

        CreateMap<PQREntity, PQRResponse>()
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

        CreateMap<CrearPQRRequest, PQREntity>()
            .ForPath(dest =>
                dest.Subject,
                opt => opt.MapFrom(src => src.Asunto))
            .ForPath(dest =>
                dest.Description,
                opt => opt.MapFrom(src => src.Descripcion))
            .ForPath(dest =>
                dest.CreateDate,
                opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<PQREntity, ConsultarPQRResponse>()
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
