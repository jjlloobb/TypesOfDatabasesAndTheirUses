namespace CBTW.Microservices.CallCenter.Domain.CallCenter;

public partial class CallCenterEntity
{
    public CallCenterEntity()
    {
        Phones = new HashSet<PhoneCCEntity>();
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<PhoneCCEntity> Phones { get; set; }
}
