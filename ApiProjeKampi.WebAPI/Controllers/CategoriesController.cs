using ApiProjeKampi.WebAPI.Context;
using ApiProjeKampi.WebAPI.Dtos.CategoryDtos;
using ApiProjeKampi.WebAPI.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProjeKampi.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ApiContext _context;
    private readonly IMapper _mapper;

    public CategoriesController(ApiContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> CategoryList(CancellationToken cancellationToken = default)
    {
        List<Category> categories = await _context.Categories.ToListAsync(cancellationToken);

        List<ResultCategoryDto> categoryDtos = _mapper.Map<List<ResultCategoryDto>>(categories);

        return Ok(categoryDtos);
    }

    [HttpGet("GetById/{id:int}")]
    public async Task<IActionResult> GetCategoryById(int id, CancellationToken cancellationToken = default)
    {
        Category? category = await _context.Categories.FindAsync(id, cancellationToken);
        if (category == null)
        {
            return NotFound();
        }

        ResultCategoryDto categoryDto = _mapper.Map<ResultCategoryDto>(category);

        return Ok(categoryDto);
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory(CreateCategoryDto createCategoryDto, CancellationToken cancellationToken = default)
    {
        Category category = _mapper.Map<Category>(createCategoryDto);
        await _context.Categories.AddAsync(category, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Created(string.Empty, category);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCategory(int id, CancellationToken cancellationToken = default)
    {
        Category? category = await _context.Categories.FindAsync(id, cancellationToken);
        if (category == null)
        {
            return NotFound();
        }
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);
        return Ok("Kategori Silme İşlemi Başarılı.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto, CancellationToken cancellationToken = default)
    {
        Category category = _mapper.Map<Category>(updateCategoryDto);
        _context.Categories.Update(category);
        await _context.SaveChangesAsync(cancellationToken);
        return Ok("Kategori Güncelleme İşlemi Başarılı.");
    }
}