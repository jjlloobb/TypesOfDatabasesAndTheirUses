using System.Linq.Expressions;

namespace CBTW.Microservices.CallCenter.Application.Providers;

public interface IRepository<T>
{
    public IQueryable<T> FindAll();
    public IQueryable<T> FindBy(Expression<Func<T, bool>> expression);
    public void Create(T entity);
    public void Update(T entity);
    public void Delete(T entity);
    public void ExecWithStoreProcedure(string sql, params object[] parameters);
}
