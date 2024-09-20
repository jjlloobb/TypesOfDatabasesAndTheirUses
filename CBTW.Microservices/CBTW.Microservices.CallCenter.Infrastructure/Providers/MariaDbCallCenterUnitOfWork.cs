using CBTW.Microservices.CallCenter.Application.Providers;
using CBTW.Microservices.CallCenter.Domain.CallCenter;

namespace CBTW.Microservices.CallCenter.Infrastructure.Providers;

public class MariaDbCallCenterUnitOfWork : IUnitOfWork
{
    private readonly MariaDbCallCenterDbContext dbContext;

    public MariaDbCallCenterUnitOfWork(MariaDbCallCenterDbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        CallCenters = new RepositoryBase<CallCenterEntity, MariaDbCallCenterDbContext>(this.dbContext);
        Phones = new RepositoryBase<PhoneCCEntity, MariaDbCallCenterDbContext>(this.dbContext);
        Countries = new RepositoryBase<CountryEntity, MariaDbCallCenterDbContext>(this.dbContext);
        Cities = new RepositoryBase<CityEntity, MariaDbCallCenterDbContext>(this.dbContext);
        DocumentTypes = new RepositoryBase<DocumentTypeEntity, MariaDbCallCenterDbContext>(this.dbContext);
        Customers = new RepositoryBase<CustomerEntity, MariaDbCallCenterDbContext>(this.dbContext);
        PQRs = new RepositoryBase<PQREntity, MariaDbCallCenterDbContext>(this.dbContext);
    }

    public IRepository<CallCenterEntity> CallCenters { get; private set; }

    public IRepository<PhoneCCEntity> Phones { get; private set; }

    public IRepository<CountryEntity> Countries { get; private set; }

    public IRepository<CityEntity> Cities { get; private set; }

    public IRepository<DocumentTypeEntity> DocumentTypes { get; private set; }

    public IRepository<CustomerEntity> Customers { get; private set; }

    public IRepository<PQREntity> PQRs { get; private set; }

    public Task CommitAsync(CancellationToken cancellationToken)
        => dbContext.SaveChangesAsync(cancellationToken);
}
