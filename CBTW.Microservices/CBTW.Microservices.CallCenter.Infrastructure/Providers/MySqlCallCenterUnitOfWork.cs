using CBTW.Microservices.CallCenter.Application.Providers;
using CBTW.Microservices.CallCenter.Domain.CallCenter;

namespace CBTW.Microservices.CallCenter.Infrastructure.Providers;

public class MySqlCallCenterUnitOfWork : IUnitOfWork
{
    private readonly MySqlCallCenterDbContext dbContext;

    public MySqlCallCenterUnitOfWork(MySqlCallCenterDbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        CallCenters = new RepositoryBase<CallCenterEntity, MySqlCallCenterDbContext>(this.dbContext);
        Phones = new RepositoryBase<PhoneCCEntity, MySqlCallCenterDbContext>(this.dbContext);
        Countries = new RepositoryBase<CountryEntity, MySqlCallCenterDbContext>(this.dbContext);
        Cities = new RepositoryBase<CityEntity, MySqlCallCenterDbContext>(this.dbContext);
        DocumentTypes = new RepositoryBase<DocumentTypeEntity, MySqlCallCenterDbContext>(this.dbContext);
        Customers = new RepositoryBase<CustomerEntity, MySqlCallCenterDbContext>(this.dbContext);
        PQRs = new RepositoryBase<PQREntity, MySqlCallCenterDbContext>(this.dbContext);
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
