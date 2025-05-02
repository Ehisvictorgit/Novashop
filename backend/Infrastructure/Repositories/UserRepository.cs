using Core.Entities;
using Core.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserRepository(IMongoCollection<User> users)
    {
        _users = users;
    }

    public Task<User> GetByIdAsync(ObjectId id)
    {
        return _users.Find(user => user.Id == id).FirstOrDefaultAsync();
    }
    public Task<IEnumerable<User>> GetAllAsync()
    {
        return _users.Find(user => true).ToListAsync().ContinueWith(task => (IEnumerable<User>)task.Result);
    }

    public IEnumerable<User> Find(Expression<Func<User, bool>> expression)
    {
        return _users.Find(expression).ToEnumerable();
    }

    public void Add(User entity)
    {
        _users.InsertOne(entity);   
    }

    public void AddRange(IEnumerable<User> entities)
    {
        _users.InsertMany(entities);
    }

    Task IGenericRepository<User>.Remove(ObjectId id)
    {
        return _users.DeleteOneAsync(user => user.Id == id);
    }

    public void RemoveRange(IEnumerable<User> entities)
    {
        _users.DeleteMany(user => entities.Select(e => e.Id).Contains(user.Id));
    }

    public void Update(User entity)
    {
        _users.ReplaceOne(user => user.Id == entity.Id, entity);
    }

    Task<User> IUserRepository.GetByUserAsync(string usr)
    {
        var user = _users.Find(user => user.Usr == usr).FirstOrDefaultAsync();
        return user;
    }
}
