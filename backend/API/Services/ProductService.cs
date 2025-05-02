using API.DTOs;
using Core.Interfaces;
using MongoDB.Bson;

namespace API.Services;

public class ProductDTOService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductDTOService(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<ProductDTO> GetByIdAsync(ObjectId productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        var category = await _categoryRepository.GetByIdAsync(product.CategoryId);

        if (product is null)
            return null;

        var productDTO = new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Image = product.Image,
            Description = product.Description,
            Category = category.Name
        };

        return productDTO;
    }

    public async Task<IEnumerable<ProductDTO>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();

        if (products is null || !products.Any())
            return [];

        var productsDTO = new List<ProductDTO>();

        foreach (var product in products)
        {
            var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
            var productDTO = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Image = product.Image,
                Description = product.Description,
                Category = category.Name
            };

            productsDTO.Add(productDTO);
        }

        return productsDTO;
    }
}
