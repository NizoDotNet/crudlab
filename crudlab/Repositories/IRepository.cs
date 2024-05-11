using Entities;
using System.Linq.Expressions;

namespace crudlab.Repositories;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T> Get(int id);
    Task Delete(int id);
    Task Update(int id, T entity);
    Task Add(T entity);
    Task<T> GetByName(string name);
    Task<List<T>> Get(Expression<Func<T, bool>> expression);

}
