using Core.Interfaces;

namespace Core.Interfaces;

public interface IUnitOfWork
{
    IProductRepository Products { get; }
    ICategoryRepository Categories { get; }

    void Dispose();
    Task<int> SaveAsync();
}
