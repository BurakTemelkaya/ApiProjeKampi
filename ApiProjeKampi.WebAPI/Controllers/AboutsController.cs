using ApiProjeKampi.WebAPI.Context;
using ApiProjeKampi.WebAPI.Dtos.AboutDtos;
using ApiProjeKampi.WebAPI.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProjeKampi.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AboutsController : ControllerBase
{
    private readonly ApiContext _context;
    private readonly IMapper _mapper;

    public AboutsController(ApiContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> AboutList(CancellationToken cancellationToken = default)
    {
        List<About> Abouts = await _context.Abouts.ToListAsync(cancellationToken);

        List<ResultAboutDto> AboutDtos = _mapper.Map<List<ResultAboutDto>>(Abouts);

        return Ok(AboutDtos);
    }

    [HttpGet("GetById/{id:int}")]
    public async Task<IActionResult> GetAboutById(int id, CancellationToken cancellationToken = default)
    {
        About? About = await _context.Abouts.FindAsync(id, cancellationToken);
        if (About == null)
        {
            return NotFound();
        }

        ResultAboutDto AboutDto = _mapper.Map<ResultAboutDto>(About);

        return Ok(AboutDto);
    }

    [HttpPost]
    public async Task<IActionResult> AddAbout(CreateAboutDto createAboutDto, CancellationToken cancellationToken = default)
    {
        About About = _mapper.Map<About>(createAboutDto);
        await _context.Abouts.AddAsync(About, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Created(string.Empty, About);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAbout(int id, CancellationToken cancellationToken = default)
    {
        About? About = await _context.Abouts.FindAsync(id, cancellationToken);
        if (About == null)
        {
            return NotFound();
        }
        _context.Abouts.Remove(About);
        await _context.SaveChangesAsync(cancellationToken);
        return Ok("Hakkımda Silme İşlemi Başarılı.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto, CancellationToken cancellationToken = default)
    {
        About About = _mapper.Map<About>(updateAboutDto);
        _context.Abouts.Update(About);
        await _context.SaveChangesAsync(cancellationToken);
        return Ok("Hakkımda Güncelleme İşlemi Başarılı.");
    }
}