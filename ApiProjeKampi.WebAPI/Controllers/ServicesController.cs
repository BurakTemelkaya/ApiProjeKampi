using ApiProjeKampi.WebAPI.Context;
using ApiProjeKampi.WebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProjeKampi.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServicesController : ControllerBase
{
    private readonly ApiContext _context;

    public ServicesController(ApiContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> ServiceList(CancellationToken cancellationToken = default)
    {
        List<Service> Services = await _context.Services.ToListAsync(cancellationToken);
        return Ok(Services);
    }

    [HttpGet("GetById{id:int}")]
    public async Task<IActionResult> GetServiceById(int id, CancellationToken cancellationToken = default)
    {
        Service? Service = await _context.Services.FindAsync(id, cancellationToken);
        if (Service == null)
        {
            return NotFound();
        }
        return Ok(Service);
    }

    [HttpPost]
    public async Task<IActionResult> AddService(Service service, CancellationToken cancellationToken = default)
    {
        await _context.Services.AddAsync(service, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Created(string.Empty, service);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteService(int id, CancellationToken cancellationToken = default)
    {
        Service? Service = await _context.Services.FindAsync(id, cancellationToken);
        if (Service == null)
        {
            return NotFound();
        }
        _context.Services.Remove(Service);
        await _context.SaveChangesAsync(cancellationToken);
        return Ok("Servis Silme İşlemi Başarılı.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateService(Service service, CancellationToken cancellationToken = default)
    {
        _context.Services.Update(service);
        await _context.SaveChangesAsync(cancellationToken);
        return Ok("Servis Güncelleme İşlemi Başarılı.");
    }
}
