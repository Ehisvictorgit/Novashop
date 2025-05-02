using API.DTOs;
using API.Services;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver.Linq;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ProductDTOService _productDTOService;

        public ProductsController(IProductRepository productRepository, IMapper mapper, ProductDTOService productDTOService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _productDTOService = productDTOService;
        }

        [HttpGet("dto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetByIdDto()
        {
            var products = await _productDTOService.GetAllAsync();

            if (products is null || !products.Any())
                return NotFound();

            var response = products.Select(product => new
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Image = product.Image,
                Category = product.Category
            }).ToList();

            return Ok(response);
        }

        [HttpGet("dto/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDTO>> GetAllDto(ObjectId id)
        {
            var product = await _productDTOService.GetByIdAsync(id);

            if (product is null)
                return NotFound();

            var response = new
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Image = product.Image,
                Category = product.Category
            };

            return Ok(response);
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? search)
        {
            var products = await _productRepository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                products = products
                    .Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return Ok(products);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _productRepository.GetAllAsync();

            if (products is null || !products.Any())
                return NotFound();


            var response = products.Select(product => new
            {
                id = product.Id.ToString(),
                product.Name,
                product.Quality,
                product.ExpirationDate,
                product.Price,
                product.Description,
                product.Currency,
                product.CategoryId,
                product.Available,
                product.Image
            }).ToList();

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetById(ObjectId id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product is null)
                return NotFound();

            var response = new
            {
                id = product.Id.ToString(),
                product.Name,
                product.Quality,
                product.ExpirationDate,
                product.Price,
                product.Description,
                product.Currency,
                product.CategoryId,
                product.Available,
                product.Image
            };

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Product> Post([FromBody] Product product)
        {
            if (product is null)
                return BadRequest();

            _productRepository.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Product> Put(string id, [FromBody] Product product)
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return BadRequest("Invalid ObjectId format.");

            product.Id = objectId;

            if (product is null)
                return NotFound();

            _productRepository.Update(product);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete( ObjectId id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product is null)
                return NotFound();

            await _productRepository.Remove(id);
            return NoContent();
        }
    }
}
