using ApiProjeKampi.WebAPI.Context;
using ApiProjeKampi.WebAPI.Dtos.NotificationDtos;
using ApiProjeKampi.WebAPI.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProjeKampi.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ApiContext _apiContext;

    public NotificationsController(IMapper mapper, ApiContext apiContext)
    {
        _mapper = mapper;
        _apiContext = apiContext;
    }

    [HttpGet]
    public async Task<IActionResult> NotificationList(CancellationToken cancellationToken = default)
    {
        List<Notification> notifications = await _apiContext.Notifications.ToListAsync(cancellationToken);
        return Ok(_mapper.Map<List<ResultNotificationDto>>(notifications));
    }

    [HttpGet("GetNotification{id:int}")]
    public async Task<IActionResult> GetNotification(int id, CancellationToken cancellationToken = default)
    {
        Notification? notification = await _apiContext.Notifications.FindAsync(id, cancellationToken);
        if (notification == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<GetByIdNotificationDto>(notification));
    }

    [HttpPost]
    public async Task<IActionResult> CreateNotification(CreateNotificationDto createNotificationDto, CancellationToken cancellationToken = default)
    {
        Notification notification = _mapper.Map<Notification>(createNotificationDto);
        await _apiContext.Notifications.AddAsync(notification, cancellationToken);
        await _apiContext.SaveChangesAsync(cancellationToken);
        return Created(string.Empty, notification);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateNotification(UpdateNotificationDto updateNotificationDto, CancellationToken cancellationToken = default)
    {
        Notification? notification = _mapper.Map<Notification>(updateNotificationDto);
        _apiContext.Notifications.Update(notification);
        await _apiContext.SaveChangesAsync(cancellationToken);
        return Ok("Güncelleme işlemi başarılı.");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteNotification(int id, CancellationToken cancellationToken = default)
    {
        Notification? notification = await _apiContext.Notifications.FindAsync(id, cancellationToken);
        if (notification == null)
        {
            return NotFound();
        }
        _apiContext.Notifications.Remove(notification);
        await _apiContext.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
}