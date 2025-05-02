using Core.Entities;
using MongoDB.Bson;
using System.Linq.Expressions;

namespace Core.Interfaces;
public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(ObjectId id);
    Task<IEnumerable<T>> GetAllAsync();
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    Task Remove(ObjectId id);
    void RemoveRange(IEnumerable<T> entities);
    void Update(T entity);
}
