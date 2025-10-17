using ApiProjeKampi.WebAPI.Context;
using ApiProjeKampi.WebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProjeKampi.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ApiContext _context;

    public CategoriesController(ApiContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> CategoryList(CancellationToken cancellationToken = default)
    {
        List<Category> categories = await _context.Categories.ToListAsync(cancellationToken);
        return Ok(categories);
    }

    [HttpGet("GetById{id:int}")]
    public async Task<IActionResult> GetCategoryById(int id, CancellationToken cancellationToken = default)
    {
        Category? category = await _context.Categories.FindAsync(id, cancellationToken);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory(Category category, CancellationToken cancellationToken = default)
    {
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
    public async Task<IActionResult> UpdateCategory(Category category, CancellationToken cancellationToken = default)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync(cancellationToken);
        return Ok("Kategori Güncelleme İşlemi Başarılı.");
    }
}