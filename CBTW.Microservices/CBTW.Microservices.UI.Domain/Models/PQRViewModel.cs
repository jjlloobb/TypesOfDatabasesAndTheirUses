using System.ComponentModel.DataAnnotations;

namespace CBTW.Microservices.UI.Domain.Models;

public class PQRViewModel
{
    [Display(Name = "No. Radicado")]
    public long Id { get; set; }

	public long IdCustomer { get; set; }

	[Display(Name = "Cliente")]
    public string Customer { get; set; }

    [Display(Name = "Número documento del cliente")]
    public string Documento { get; set; }

    [Display(Name = "Nombre completo del cliente")]
    public string NombreCompleto { get; set; }

    [Display(Name = "Asunto")]
    [StringLength(200, MinimumLength = 1)]
    [Required]
    public string Asunto { get; set; }

    [Display(Name = "Descripción")]
    [StringLength(4000, MinimumLength = 1)]
    [Required]
    public string Descripcion { get; set; }
}