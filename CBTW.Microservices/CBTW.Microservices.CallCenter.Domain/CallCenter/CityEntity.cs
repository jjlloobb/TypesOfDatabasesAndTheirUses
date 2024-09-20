namespace CBTW.Microservices.CallCenter.Domain.CallCenter;

public partial class CityEntity
{
    public CityEntity()
    {
        Customers = new HashSet<CustomerEntity>();
    }

    public int Id { get; set; }

    public int IdCountry { get; set; }

    public string Name { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual CountryEntity IdCountryNavigation { get; set; }

    public virtual ICollection<CustomerEntity> Customers { get; set; }
}
