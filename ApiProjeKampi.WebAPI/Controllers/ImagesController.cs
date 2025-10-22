using ApiProjeKampi.WebAPI.Context;
using ApiProjeKampi.WebAPI.Dtos.ImageDtos;
using ApiProjeKampi.WebAPI.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProjeKampi.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImagesController : ControllerBase
{
    private readonly ApiContext _context;
    private readonly IMapper _mapper;

    public ImagesController(ApiContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> ImageList(CancellationToken cancellationToken = default)
    {
        List<Image> Images = await _context.Images.ToListAsync(cancellationToken);

        List<ResultImageDto> ImageDtos = _mapper.Map<List<ResultImageDto>>(Images);

        return Ok(ImageDtos);
    }

    [HttpGet("GetById/{id:int}")]
    public async Task<IActionResult> GetImageById(int id, CancellationToken cancellationToken = default)
    {
        Image? Image = await _context.Images.FindAsync(id, cancellationToken);
        if (Image == null)
        {
            return NotFound();
        }

        ResultImageDto ImageDto = _mapper.Map<ResultImageDto>(Image);

        return Ok(ImageDto);
    }

    [HttpPost]
    public async Task<IActionResult> AddImage(CreateImageDto createImageDto, CancellationToken cancellationToken = default)
    {
        Image Image = _mapper.Map<Image>(createImageDto);
        await _context.Images.AddAsync(Image, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Created(string.Empty, Image);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteImage(int id, CancellationToken cancellationToken = default)
    {
        Image? Image = await _context.Images.FindAsync(id, cancellationToken);
        if (Image == null)
        {
            return NotFound();
        }
        _context.Images.Remove(Image);
        await _context.SaveChangesAsync(cancellationToken);
        return Ok("Görsel Silme İşlemi Başarılı.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateImage(UpdateImageDto updateImageDto, CancellationToken cancellationToken = default)
    {
        Image Image = _mapper.Map<Image>(updateImageDto);
        _context.Images.Update(Image);
        await _context.SaveChangesAsync(cancellationToken);
        return Ok("Görsel Güncelleme İşlemi Başarılı.");
    }
}