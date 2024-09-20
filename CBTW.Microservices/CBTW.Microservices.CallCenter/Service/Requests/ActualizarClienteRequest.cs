using CBTW.Microservices.CallCenter.Service.Responses;
using MediatR;

namespace CBTW.Microservices.CallCenter.Service.Requests;

public class ActualizarClienteRequest : IRequest<ActualizarClienteResponse>
{
    public int TipoDocumento { get; set; }

    public string Documento { get; set; }

    public string NombreCompleto { get; set; }

    public string CodigoPaisCelular { get; set; }

    public string Celular { get; set; }

    public int Ciudad { get; set; }        

    public DateTime FechaNacimiento { get; set; }

    public int TelefonoCC { get; set; }
}
