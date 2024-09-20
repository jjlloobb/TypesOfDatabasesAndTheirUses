using CBTW.Microservices.CallCenter.Domain.CallCenter;

namespace CBTW.Microservices.CallCenter.Application.Providers;

public interface IUnitOfWork
{
    public IRepository<CallCenterEntity> CallCenters { get; }

    public IRepository<PhoneCCEntity> Phones { get; }

    public IRepository<CountryEntity> Countries { get; }

    public IRepository<CityEntity> Cities { get; }

    public IRepository<DocumentTypeEntity> DocumentTypes { get; }

    public IRepository<CustomerEntity> Customers { get; }

    public IRepository<PQREntity> PQRs { get; }

    public Task CommitAsync(CancellationToken cancellationToken);
}
