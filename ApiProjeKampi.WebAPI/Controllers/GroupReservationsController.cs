using ApiProjeKampi.WebAPI.Context;
using ApiProjeKampi.WebAPI.Dtos.GroupReservationDtos;
using ApiProjeKampi.WebAPI.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProjeKampi.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupReservationsController : ControllerBase
{
    private readonly ApiContext _context;
    private readonly IMapper _mapper;

    public GroupReservationsController(ApiContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GroupReservationList(CancellationToken cancellationToken = default)
    {
        List<GroupReservation> groupReservations = await _context.GroupReservations.ToListAsync(cancellationToken);

        List<ResultGroupReservationDto> groupReservationDtos = _mapper.Map<List<ResultGroupReservationDto>>(groupReservations);

        return Ok(groupReservationDtos);
    }

    [HttpGet("GetById/{id:int}")]
    public async Task<IActionResult> GetGroupReservationById(int id, CancellationToken cancellationToken = default)
    {
        GroupReservation? groupReservation = await _context.GroupReservations.FindAsync(id, cancellationToken);
        if (groupReservation == null)
        {
            return NotFound();
        }

        ResultGroupReservationDto groupReservationDto = _mapper.Map<ResultGroupReservationDto>(groupReservation);

        return Ok(groupReservationDto);
    }

    [HttpPost]
    public async Task<IActionResult> AddGroupReservation(CreateGroupReservationDto createGroupReservationDto, CancellationToken cancellationToken = default)
    {
        GroupReservation groupReservation = _mapper.Map<GroupReservation>(createGroupReservationDto);
        await _context.GroupReservations.AddAsync(groupReservation, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Created(string.Empty, groupReservation);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteGroupReservation(int id, CancellationToken cancellationToken = default)
    {
        GroupReservation? groupReservation = await _context.GroupReservations.FindAsync(id, cancellationToken);
        if (groupReservation == null)
        {
            return NotFound();
        }
        _context.GroupReservations.Remove(groupReservation);
        await _context.SaveChangesAsync(cancellationToken);
        return Ok("Silme İşlemi Başarılı.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateGroupReservation(UpdateGroupReservationDto updateGroupReservationDto, CancellationToken cancellationToken = default)
    {
        GroupReservation groupReservation = _mapper.Map<GroupReservation>(updateGroupReservationDto);
        _context.GroupReservations.Update(groupReservation);
        await _context.SaveChangesAsync(cancellationToken);
        return Ok("Güncelleme İşlemi Başarılı.");
    }
}