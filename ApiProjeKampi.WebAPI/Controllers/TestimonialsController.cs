using ApiProjeKampi.WebAPI.Context;
using ApiProjeKampi.WebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProjeKampi.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestimonialsController : ControllerBase
{
    private readonly ApiContext _context;

    public TestimonialsController(ApiContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> TestimonialList(CancellationToken cancellationToken = default)
    {
        List<Testimonial> Testimonials = await _context.Testimonials.ToListAsync(cancellationToken);
        return Ok(Testimonials);
    }

    [HttpGet("GetById{id:int}")]
    public async Task<IActionResult> GetTestimonialById(int id, CancellationToken cancellationToken = default)
    {
        Testimonial? Testimonial = await _context.Testimonials.FindAsync(id, cancellationToken);
        if (Testimonial == null)
        {
            return NotFound();
        }
        return Ok(Testimonial);
    }

    [HttpPost]
    public async Task<IActionResult> AddTestimonial(Testimonial Testimonial, CancellationToken cancellationToken = default)
    {
        await _context.Testimonials.AddAsync(Testimonial, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Created(string.Empty, Testimonial);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTestimonial(int id, CancellationToken cancellationToken = default)
    {
        Testimonial? Testimonial = await _context.Testimonials.FindAsync(id, cancellationToken);
        if (Testimonial == null)
        {
            return NotFound();
        }
        _context.Testimonials.Remove(Testimonial);
        await _context.SaveChangesAsync(cancellationToken);
        return Ok("Referans Silme İşlemi Başarılı.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTestimonial(Testimonial Testimonial, CancellationToken cancellationToken = default)
    {
        _context.Testimonials.Update(Testimonial);
        await _context.SaveChangesAsync(cancellationToken);
        return Ok("Referans Güncelleme İşlemi Başarılı.");
    }
}
