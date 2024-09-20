namespace CBTW.Microservices.CallCenter.Domain.CallCenter;

public partial class CountryEntity
{
    public CountryEntity()
    {
        Cities = new HashSet<CityEntity>();
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<CityEntity> Cities { get; set; }
}
