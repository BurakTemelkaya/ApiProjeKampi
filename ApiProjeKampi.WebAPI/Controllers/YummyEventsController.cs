using ApiProjeKampi.WebAPI.Context;
using ApiProjeKampi.WebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProjeKampi.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class YummyEventsController : ControllerBase
{
    private readonly ApiContext _context;

    public YummyEventsController(ApiContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> YummyEventList(CancellationToken cancellationToken = default)
    {
        List<YummyEvent> YummyEvents = await _context.YummyEvents.ToListAsync(cancellationToken);
        return Ok(YummyEvents);
    }

    [HttpGet("GetYummyEvent{id:int}")]
    public async Task<IActionResult> GetYummyEventById(int id, CancellationToken cancellationToken = default)
    {
        YummyEvent? YummyEvent = await _context.YummyEvents.FindAsync(id, cancellationToken);
        if (YummyEvent == null)
        {
            return NotFound();
        }
        return Ok(YummyEvent);
    }

    [HttpPost]
    public async Task<IActionResult> AddYummyEvent(YummyEvent YummyEvent, CancellationToken cancellationToken = default)
    {
        await _context.YummyEvents.AddAsync(YummyEvent, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Created(string.Empty, YummyEvent);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteYummyEvent(int id, CancellationToken cancellationToken = default)
    {
        YummyEvent? YummyEvent = await _context.YummyEvents.FindAsync(id, cancellationToken);
        if (YummyEvent == null)
        {
            return NotFound();
        }
        _context.YummyEvents.Remove(YummyEvent);
        await _context.SaveChangesAsync(cancellationToken);
        return Ok("Etkinlik Silme İşlemi Başarılı.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateYummyEvent(YummyEvent YummyEvent, CancellationToken cancellationToken = default)
    {
        _context.YummyEvents.Update(YummyEvent);
        await _context.SaveChangesAsync(cancellationToken);
        return Ok("Etkinlik Güncelleme İşlemi Başarılı.");
    }
}
