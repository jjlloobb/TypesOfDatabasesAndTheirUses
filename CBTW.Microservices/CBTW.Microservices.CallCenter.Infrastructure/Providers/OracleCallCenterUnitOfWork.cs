using CBTW.Microservices.CallCenter.Application.Providers;
using CBTW.Microservices.CallCenter.Domain.CallCenter;

namespace CBTW.Microservices.CallCenter.Infrastructure.Providers;

public class OracleCallCenterUnitOfWork : IUnitOfWork
{
    private readonly OracleCallCenterDbContext dbContext;

    public OracleCallCenterUnitOfWork(OracleCallCenterDbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        CallCenters = new RepositoryBase<CallCenterEntity, OracleCallCenterDbContext>(this.dbContext);
        Phones = new RepositoryBase<PhoneCCEntity, OracleCallCenterDbContext>(this.dbContext);
        Countries = new RepositoryBase<CountryEntity, OracleCallCenterDbContext>(this.dbContext);
        Cities = new RepositoryBase<CityEntity, OracleCallCenterDbContext>(this.dbContext);
        DocumentTypes = new RepositoryBase<DocumentTypeEntity, OracleCallCenterDbContext>(this.dbContext);
        Customers = new RepositoryBase<CustomerEntity, OracleCallCenterDbContext>(this.dbContext);
        PQRs = new RepositoryBase<PQREntity, OracleCallCenterDbContext>(this.dbContext);
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
