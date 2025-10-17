using ApiProjeKampi.WebAPI.Context;
using ApiProjeKampi.WebAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProjeKampi.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChefsController : ControllerBase
{
    private readonly ApiContext _context;

    public ChefsController(ApiContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> ChefList(CancellationToken cancellationToken = default)
    {
        List<Chef> chefs = await _context.Chefs.ToListAsync(cancellationToken);
        return Ok(chefs);
    }

    [HttpGet("GetChef{id:int}")]
    public async Task<IActionResult> GetChefById(int id, CancellationToken cancellationToken = default)
    {
        Chef? chef = await _context.Chefs.FindAsync(id, cancellationToken);
        if (chef == null)
        {
            return NotFound();
        }
        return Ok(chef);
    }

    [HttpPost]
    public async Task<IActionResult> AddChef(Chef chef, CancellationToken cancellationToken = default)
    {
        await _context.Chefs.AddAsync(chef, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Created(string.Empty, chef);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteChef(int id, CancellationToken cancellationToken = default)
    {
        Chef? chef = await _context.Chefs.FindAsync(id, cancellationToken);
        if (chef == null)
        {
            return NotFound();
        }
        _context.Chefs.Remove(chef);
        await _context.SaveChangesAsync(cancellationToken);
        return Ok("Chef Silme İşlemi Başarılı.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateChef(Chef chef, CancellationToken cancellationToken = default)
    {
        _context.Chefs.Update(chef);
        await _context.SaveChangesAsync(cancellationToken);
        return Ok("Chef Güncelleme İşlemi Başarılı.");
    }
}
