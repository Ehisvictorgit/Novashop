using Core.Entities;
using Core.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _products;

    public ProductRepository(IMongoCollection<Product> products)
    {
        _products = products;
    }

    public void Add(Product product)
    {
        _products.InsertOne(product);
    }

    public void AddRange(IEnumerable<Product> products)
    {
        _products.InsertMany(products);
    }
    public IEnumerable<Product> Find(Expression<Func<Product, bool>> expression)
    {
        return _products.Find(expression).ToEnumerable();
    }
    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return _products.Find(product => true).ToListAsync().ContinueWith(task => (IEnumerable<Product>)task.Result);
    }

    public Task<Product> GetByIdAsync(ObjectId id)
    {
        return _products.Find(product => product.Id == id).FirstOrDefaultAsync();
    }

    Task IGenericRepository<Product>.Remove(ObjectId id)
    {
        return _products.DeleteOneAsync(p => p.Id == id);
    }

    public void RemoveRange(IEnumerable<Product> entities)
    {
        _products.DeleteMany(p => entities.Select(e => e.Id).Contains(p.Id));
    }

    public void Update(Product entity)
    {
        _products.ReplaceOne(p => p.Id == entity.Id, entity);
    }
}
