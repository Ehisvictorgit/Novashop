using Core.Entities;
using Core.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly IMongoDatabase _database;
    private IProductRepository _products;
    private ICategoryRepository _categories;
    private IUserRepository _users;

    public UnitOfWork(IMongoClient mongoClient, IConfiguration configuration)
    {
        var databaseName = configuration["ProductDBName"];
        _database = mongoClient.GetDatabase(databaseName);
    }

    public IProductRepository Products
    {
        get
        {
            if (_products is null)
                _products = new Repositories.ProductRepository(_database.GetCollection<Product>("Product"));

            return _products;
        }
    }

    public Core.Interfaces.ICategoryRepository Categories
    {
        get
        {
            if (_categories is null)
                _categories = new CategoryRepository(_database.GetCollection<Category>("Category"));

            return _categories;
        }
    }

    public IUserRepository Users
    {
        get
        {
            if (_users is null)
                _users = new UserRepository(_database.GetCollection<User>("User"));
            return _users;
        }
    }

    public void Dispose()
    {
    }

    public Task<int> SaveAsync()
    {
        return Task.FromResult(1);
    }
}