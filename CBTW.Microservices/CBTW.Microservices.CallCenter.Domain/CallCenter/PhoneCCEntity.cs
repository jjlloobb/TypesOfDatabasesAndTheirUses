namespace CBTW.Microservices.CallCenter.Domain.CallCenter;

public partial class PhoneCCEntity
{
    public PhoneCCEntity()
    {
        Customers = new HashSet<CustomerEntity>();
    }

    public int Id { get; set; }

    public int IdCallCenter { get; set; }

    public string CountryCode { get; set; }

    public string PhoneNumber { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual CallCenterEntity IdCallCenterNavigation { get; set; }

    public virtual ICollection<CustomerEntity> Customers { get; set; }
}
