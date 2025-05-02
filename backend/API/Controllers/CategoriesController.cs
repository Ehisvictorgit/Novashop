using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace API.Controllers;

public class CategoriesController : BaseApiController
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoriesController(ICategoryRepository productRepository, IMapper mapper)
    {
        _categoryRepository = productRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Category>>> Get()
    {
        var categories = await _categoryRepository.GetAllAsync();

        if (categories is null || !categories.Any())
            return NotFound();

        var response = categories.Select(category => new
        {
            id = category.Id.ToString(),
            category.Name,
            category.Icon
        }).ToList();

        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Category>> Get(ObjectId id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category is null)
            return NotFound();

        var response = new
        {
            id = category.Id.ToString(),
            category.Name,
            category.Icon
        };

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Category> Post([FromBody] Category category)
    {
        if (category is null)
            return BadRequest();

        _categoryRepository.Add(category);
        return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Category> Put(string id, [FromBody] Category category)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return BadRequest("Invalid ObjectId format.");

        category.Id = objectId;

        if (category is null)
            return NotFound();

        _categoryRepository.Update(category);
        return Ok(category);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(ObjectId id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category is null)
            return NotFound();

        await _categoryRepository.Remove(id);
        return NoContent();
    }
}
