namespace CBTW.Microservices.CallCenter.Domain.CallCenter;

public partial class DocumentTypeEntity
{
    public DocumentTypeEntity()
    {
        Customers = new HashSet<CustomerEntity>();
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<CustomerEntity> Customers { get; set; }
}
