namespace CBTW.Microservices.HumanResources.Domain.HumanResources;

public class DepartmentEntity
{
	public string Abbreviation { get; set; }

	public string FullName { get; set; }

	public string Description { get; set; }

	public string CountryCode { get; set; }

	public string PhoneNumber { get; set; }

	public DateTime CreateDate { get; set; }

	public DateTime? UpdateDate { get; set; }
}
