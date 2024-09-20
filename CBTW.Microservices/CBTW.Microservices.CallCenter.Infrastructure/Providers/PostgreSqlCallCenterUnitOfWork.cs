using CBTW.Microservices.CallCenter.Application.Providers;
using CBTW.Microservices.CallCenter.Domain.CallCenter;

namespace CBTW.Microservices.CallCenter.Infrastructure.Providers;

public class PostgreSqlCallCenterUnitOfWork : IUnitOfWork
{
    private readonly PostgreSqlCallCenterDbContext dbContext;

    public PostgreSqlCallCenterUnitOfWork(PostgreSqlCallCenterDbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        CallCenters = new RepositoryBase<CallCenterEntity, PostgreSqlCallCenterDbContext>(this.dbContext);
        Phones = new RepositoryBase<PhoneCCEntity, PostgreSqlCallCenterDbContext>(this.dbContext);
        Countries = new RepositoryBase<CountryEntity, PostgreSqlCallCenterDbContext>(this.dbContext);
        Cities = new RepositoryBase<CityEntity, PostgreSqlCallCenterDbContext>(this.dbContext);
        DocumentTypes = new RepositoryBase<DocumentTypeEntity, PostgreSqlCallCenterDbContext>(this.dbContext);
        Customers = new RepositoryBase<CustomerEntity, PostgreSqlCallCenterDbContext>(this.dbContext);
        PQRs = new RepositoryBase<PQREntity, PostgreSqlCallCenterDbContext>(this.dbContext);
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
