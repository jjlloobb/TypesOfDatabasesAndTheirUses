using CBTW.Microservices.CallCenter.Application.Providers;
using CBTW.Microservices.CallCenter.Domain.CallCenter;

namespace CBTW.Microservices.CallCenter.Infrastructure.Providers;

public class SqlServerCallCenterUnitOfWork : IUnitOfWork
{
    private readonly SqlServerCallCenterDbContext dbContext;

    public SqlServerCallCenterUnitOfWork(SqlServerCallCenterDbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        CallCenters = new RepositoryBase<CallCenterEntity, SqlServerCallCenterDbContext>(this.dbContext);
        Phones = new RepositoryBase<PhoneCCEntity, SqlServerCallCenterDbContext>(this.dbContext);
        Countries = new RepositoryBase<CountryEntity, SqlServerCallCenterDbContext>(this.dbContext);
        Cities = new RepositoryBase<CityEntity, SqlServerCallCenterDbContext>(this.dbContext);
        DocumentTypes = new RepositoryBase<DocumentTypeEntity, SqlServerCallCenterDbContext>(this.dbContext);
        Customers = new RepositoryBase<CustomerEntity, SqlServerCallCenterDbContext>(this.dbContext);
        PQRs = new RepositoryBase<PQREntity, SqlServerCallCenterDbContext>(this.dbContext);
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
