using ApiProjeKampi.WebAPI.Context;
using ApiProjeKampi.WebAPI.Dtos.ReservationDtos;
using ApiProjeKampi.WebAPI.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProjeKampi.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly ApiContext _context;
    private readonly IMapper _mapper;

    public ReservationsController(ApiContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> ReservationList(CancellationToken cancellationToken = default)
    {
        List<Reservation> reservations = await _context.Reservations.ToListAsync(cancellationToken);

        List<ResultReservationDto> reservationDtos = _mapper.Map<List<ResultReservationDto>>(reservations);

        return Ok(reservationDtos);
    }

    [HttpGet("GetById/{id:int}")]
    public async Task<IActionResult> GetReservationById(int id, CancellationToken cancellationToken = default)
    {
        Reservation? reservation = await _context.Reservations.FindAsync(id, cancellationToken);
        if (reservation == null)
        {
            return NotFound();
        }

        ResultReservationDto reservationDto = _mapper.Map<ResultReservationDto>(reservation);

        return Ok(reservationDto);
    }

    [HttpPost]
    public async Task<IActionResult> AddReservation(CreateReservationDto createReservationDto, CancellationToken cancellationToken = default)
    {
        Reservation reservation = _mapper.Map<Reservation>(createReservationDto);
        await _context.Reservations.AddAsync(reservation, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Created(string.Empty, reservation);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteReservation(int id, CancellationToken cancellationToken = default)
    {
        Reservation? reservation = await _context.Reservations.FindAsync(id, cancellationToken);
        if (reservation == null)
        {
            return NotFound();
        }
        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync(cancellationToken);
        return Ok("Reservasyon Silme İşlemi Başarılı.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateReservation(UpdateReservationDto updateReservationDto, CancellationToken cancellationToken = default)
    {
        Reservation Reservation = _mapper.Map<Reservation>(updateReservationDto);
        _context.Reservations.Update(Reservation);
        await _context.SaveChangesAsync(cancellationToken);
        return Ok("Reservasyon Güncelleme İşlemi Başarılı.");
    }
}