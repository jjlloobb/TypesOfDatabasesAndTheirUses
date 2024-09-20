using System.ComponentModel.DataAnnotations;

namespace CBTW.Microservices.UI.Domain.Models;

public class CustomerViewModel
{
	public long IdCustomer { get; set; }

	public string Id { get; set; }

    public int IdTipoDocumento { get; set; }

    [Display(Name = "Tipo documento")]
    public string TipoDocumento { get; set; }
            
    [Display(Name = "Número documento")]
    [StringLength(20, MinimumLength = 1)]
    [Required]
    public string Documento { get; set; }
            
    [Display(Name = "Nombre completo")]
    [StringLength(200, MinimumLength = 1)]
    [Required]
    public string NombreCompleto { get; set; }
            
    [Display(Name = "Codigo Pais celular")]
    [StringLength(5, MinimumLength = 1)]
    [Required]
    public string CodigoPaisCelular { get; set; }
            
    [Display(Name = "Celular")]
    [StringLength(15, MinimumLength = 1)]
    [Required]
    public string Celular { get; set; }

    public int IdCiudad { get; set; }
            
    [Display(Name = "Ciudad")]
    public string Ciudad { get; set; }

    [Display(Name = "Fecha de nacimiento")]
    [DataType(DataType.Date)]
    [Required]
    public DateTime FechaNacimiento { get; set; }

    public int IdTelefonoCC { get; set; }

    [Display(Name = "Telefono call center")]
    public string TelefonoCC { get; set; }
}