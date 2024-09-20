namespace CBTW.Microservices.HumanResources.Domain.HumanResources;

public class EmployeeEntity
{
	public string DocumentType { get; set; }

	public string Document { get; set; }

	public string FullName { get; set; }

	public string CountryCode { get; set; }

	public string PhoneNumber { get; set; }

	public DateTime DateOfBirth { get; set; }

    public DateTime CreateDate { get; set; }

	public DateTime? UpdateDate { get; set; }
}
