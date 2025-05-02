using Core.Entities;
using Core.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly IMongoCollection<Category> _categories;
    public CategoryRepository(IMongoCollection<Category> categories)
    {
        _categories = categories;
    }

    public void Add(Category category)
    {
        _categories.InsertOne(category);
    }

    public void AddRange(IEnumerable<Category> categories)
    {
        _categories.InsertMany(categories);
    }

    public IEnumerable<Category> Find(Expression<Func<Category, bool>> expression)
    {
        return _categories.Find(expression).ToEnumerable();
    }

    public Task<IEnumerable<Category>> GetAllAsync()
    {
        return _categories.Find(category => true).ToListAsync().ContinueWith(task => (IEnumerable<Category>)task.Result);
    }

    public Task<Category> GetByIdAsync(ObjectId id)
    {
        return _categories.Find(category => category.Id == id).FirstOrDefaultAsync();
    }

    public void Remove(ObjectId id)
    {
        _categories.DeleteOne(p => p.Id == id);
    }

    Task IGenericRepository<Category>.Remove(ObjectId id)
    {
        return _categories.DeleteOneAsync(p => p.Id == id);
    }

    public void RemoveRange(IEnumerable<Category> categories)
    {
        _categories.DeleteMany(p => categories.Select(e => e.Id).Contains(p.Id));
    }

    public void Update(Category category)
    {
        _categories.ReplaceOne(p => p.Id == category.Id, category);
    }
}
