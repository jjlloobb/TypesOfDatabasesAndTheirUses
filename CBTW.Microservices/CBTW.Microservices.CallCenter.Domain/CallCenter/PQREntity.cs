namespace CBTW.Microservices.CallCenter.Domain.CallCenter;

public partial class PQREntity
{
    public long Id { get; set; }

    public long IdCustomer { get; set; }

    public string Subject { get; set; }

    public string Description { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual CustomerEntity IdCustomerNavigation { get; set; }
}
