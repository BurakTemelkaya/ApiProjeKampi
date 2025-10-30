using ApiProjeKampi.WebAPI.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ApiProjeKampi.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatisticsController : ControllerBase
{
    private readonly ApiContext _context;

    public StatisticsController(ApiContext context)
    {
        _context = context;
    }

    [HttpGet("ProductCount")]
    public async Task<IActionResult> ProductCount(CancellationToken cancellationToken = default)
    {
        int count = await _context.Products.CountAsync(cancellationToken);

        return Ok(count);
    }


    [HttpGet("ReservationCount")]
    public async Task<IActionResult> ReservationCount(CancellationToken cancellationToken = default)
    {
        int count = await _context.Reservations.CountAsync(cancellationToken);

        return Ok(count);
    }

    [HttpGet("ChefCount")]
    public async Task<IActionResult> ChefCount(CancellationToken cancellationToken = default)
    {
        int count = await _context.Chefs.CountAsync(cancellationToken);

        return Ok(count);
    }

    [HttpGet("TotalGuestCount")]
    public async Task<IActionResult> TotalGuestCount(CancellationToken cancellationToken = default)
    {
        int count = await _context.Reservations.SumAsync(x => x.CountOfPeople, cancellationToken);

        return Ok(count);
    }
}
