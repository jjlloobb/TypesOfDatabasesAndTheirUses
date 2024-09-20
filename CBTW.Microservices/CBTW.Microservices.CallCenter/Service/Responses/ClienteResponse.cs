namespace CBTW.Microservices.CallCenter.Service.Responses;

public class ClienteResponse
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
