namespace CBTW.Microservices.CallCenter.Domain.CallCenter;

public partial class CustomerEntity
{
    public CustomerEntity()
    {
        PQRs = new HashSet<PQREntity>();
    }

    public long Id { get; set; }

    public int IdDocumentType { get; set; }

    public string Document { get; set; }

    public string FullName { get; set; }

    public string CountryCode { get; set; }

    public string PhoneNumber { get; set; }

    public DateTime DateOfBirth { get; set; }

    public int IdCity { get; set; }

    public int IdPhoneCC { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual DocumentTypeEntity IdDocumentTypeNavigation { get; set; }

    public virtual CityEntity IdCityNavigation { get; set; }

    public virtual PhoneCCEntity IdPhoneCCNavigation { get; set; }

    public virtual ICollection<PQREntity> PQRs { get; set; }
}
