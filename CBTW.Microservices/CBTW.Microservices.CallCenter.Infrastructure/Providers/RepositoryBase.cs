using CBTW.Microservices.CallCenter.Application.Providers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CBTW.Microservices.CallCenter.Infrastructure.Providers;

public class RepositoryBase<TEntity, TContext> : IRepository<TEntity>
    where TEntity : class
    where TContext : DbContext
{
    protected TContext DbContext { get; set; }

    public RepositoryBase(TContext dbContext)
        => DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public IQueryable<TEntity> FindAll()
        => DbContext.Set<TEntity>().AsNoTracking();

    public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> expression)
        => DbContext.Set<TEntity>().Where(expression).AsNoTracking();

    public void Create(TEntity entity)
        => DbContext.Set<TEntity>().Add(entity);

    public void Update(TEntity entity)
        => DbContext.Set<TEntity>().Update(entity);

    public void Delete(TEntity entity)
        => DbContext.Set<TEntity>().Remove(entity);

    public void ExecWithStoreProcedure(string sql, params object[] parameters)
        => DbContext.Database.ExecuteSqlRaw($"EXEC {sql}", parameters);
}
