using ApiProjeKampi.WebAPI.Context;
using ApiProjeKampi.WebAPI.Dtos.FeatureDtos;
using ApiProjeKampi.WebAPI.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProjeKampi.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FeaturesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ApiContext _apiContext;

    public FeaturesController(IMapper mapper, ApiContext apiContext)
    {
        _mapper = mapper;
        _apiContext = apiContext;
    }

    [HttpGet]
    public async Task<IActionResult> FeatureList(CancellationToken cancellationToken = default)
    {
        List<Feature> features = await _apiContext.Features.ToListAsync(cancellationToken);
        return Ok(_mapper.Map<List<ResultFeatureDto>>(features));
    }

    [HttpGet("GetFeature/{id:int}")]
    public async Task<IActionResult> GetFeature(int id, CancellationToken cancellationToken = default)
    {
        Feature? feature = await _apiContext.Features.FindAsync(id, cancellationToken);
        if (feature == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<GetByIdFeatureDto>(feature));
    }

    [HttpPost]
    public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto, CancellationToken cancellationToken = default)
    {
        Feature feature = _mapper.Map<Feature>(createFeatureDto);
        await _apiContext.Features.AddAsync(feature, cancellationToken);
        await _apiContext.SaveChangesAsync(cancellationToken);
        return Created(string.Empty, feature);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto, CancellationToken cancellationToken = default)
    {
        Feature? feature = _mapper.Map<Feature>(updateFeatureDto);
        _apiContext.Features.Update(feature);
        await _apiContext.SaveChangesAsync(cancellationToken);
        return Ok("Güncelleme işlemi başarılı.");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteFeature(int id, CancellationToken cancellationToken = default)
    {
        Feature? feature = await _apiContext.Features.FindAsync(id, cancellationToken);
        if (feature == null)
        {
            return NotFound();
        }
        _apiContext.Features.Remove(feature);
        await _apiContext.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
}
