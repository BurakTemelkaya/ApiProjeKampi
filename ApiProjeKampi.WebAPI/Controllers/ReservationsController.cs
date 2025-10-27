using ApiProjeKampi.WebAPI.Context;
using ApiProjeKampi.WebAPI.Dtos.ReservationDtos;
using ApiProjeKampi.WebAPI.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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

    [HttpGet("GetTotalReservationCount")]
    public async Task<IActionResult> GetTotalReservationCount(CancellationToken cancellationToken = default)
    {
        int totalReservations = await _context.Reservations.CountAsync(cancellationToken);

        return Ok(totalReservations);
    }

    [HttpGet("GetTotalCustomerCount")]
    public async Task<IActionResult> GetTotalCustomerCount(CancellationToken cancellationToken = default)
    {
        int totalReservations = await _context.Reservations.SumAsync(x => x.CountOfPeople, cancellationToken);

        return Ok(totalReservations);
    }

    [HttpGet("GetPendingReservation")]
    public async Task<IActionResult> GetPendingReservation(CancellationToken cancellationToken = default)
    {
        int totalReservations = await _context.Reservations.CountAsync(x => x.ReservationStatus == "Onay Bekliyor", cancellationToken);

        return Ok(totalReservations);
    }

    [HttpGet("GetApprovedReservation")]
    public async Task<IActionResult> GetApprovedReservation(CancellationToken cancellationToken = default)
    {
        int totalReservations = await _context.Reservations.CountAsync(x => x.ReservationStatus == "Onaylandı", cancellationToken);

        return Ok(totalReservations);
    }

    [HttpGet("GetReservationStats")]
    public async Task<IActionResult> GetReservationStats(CancellationToken cancellationToken = default)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        DateOnly fourMonthsAgo = today.AddMonths(-3);

        // 1. SQL tarafında sadece gruplama ve veri çekme
        var rawData = await _context.Reservations
            .Where(r => r.ReservationDate >= fourMonthsAgo)
            .GroupBy(r => new { r.ReservationDate.Year, r.ReservationDate.Month })
            .Select(g => new
            {
                g.Key.Year,
                g.Key.Month,
                Approved = g.Count(x => x.ReservationStatus == "Onaylandı"),
                Pending = g.Count(x => x.ReservationStatus == "Onay Bekliyor"),
                Canceled = g.Count(x => x.ReservationStatus == "İptal Edildi")
            })
            .OrderBy(x => x.Year).ThenBy(x => x.Month)
            .ToListAsync(cancellationToken); // Burada SQL biter, veriler RAM’e alınır

        // 2. Bellekte DTO'ya mapleme + tarih formatlama
        List<ReservationChartDto> result = rawData.Select(x => new ReservationChartDto
        {
            Month = new DateTime(x.Year, x.Month, 1).ToString("MMMM yyyy"),
            Approved = x.Approved,
            Pending = x.Pending,
            Canceled = x.Canceled
        }).ToList();

        return Ok(result);
    }
}