using ApiProjeKampi.WebAPI.Context;
using ApiProjeKampi.WebAPI.Dtos.MessageDtos;
using ApiProjeKampi.WebAPI.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProjeKampi.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessagesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ApiContext _apiContext;

    public MessagesController(IMapper mapper, ApiContext apiContext)
    {
        _mapper = mapper;
        _apiContext = apiContext;
    }

    [HttpGet]
    public async Task<IActionResult> MessageList(CancellationToken cancellationToken = default)
    {
        List<Message> value = await _apiContext.Messages.ToListAsync(cancellationToken);
        return Ok(_mapper.Map<List<ResultMessageDto>>(value));
    }

    [HttpGet("GetMessage/{id:int}")]
    public async Task<IActionResult> GetMessage(int id, CancellationToken cancellationToken = default)
    {
        Message? message = await _apiContext.Messages.FindAsync(id, cancellationToken);
        if (message == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<ResultMessageDto>(message));
    }

    [HttpPost]
    public async Task<IActionResult> CreateMessage(CreateMessageDto createMessageDto, CancellationToken cancellationToken = default)
    {
        Message message = _mapper.Map<Message>(createMessageDto);
        await _apiContext.Messages.AddAsync(message, cancellationToken);
        await _apiContext.SaveChangesAsync(cancellationToken);
        return Created(string.Empty, message);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateMessage(UpdateMessageDto updateMessageDto, CancellationToken cancellationToken = default)
    {
        Message message = _mapper.Map<Message>(updateMessageDto);

        _apiContext.Messages.Update(message);

        await _apiContext.SaveChangesAsync(cancellationToken);
        return Ok("Mesaj güncelleme işlemi başarılı.");
    }


    [HttpDelete]
    public async Task<IActionResult> DeleteMessage(int id, CancellationToken cancellationToken = default)
    {
        Message? message = await _apiContext.Messages.FindAsync(id, cancellationToken);
        if (message == null)
        {
            return NotFound();
        }
        _apiContext.Messages.Remove(message);
        await _apiContext.SaveChangesAsync(cancellationToken);
        return Ok("Mesaj silme işlemi başarılı.");
    }

    [HttpGet("MessageListByUnreadMessages")]
    public async Task<IActionResult> MessageListByUnreadMessages()
    {
        List<Message> value = await _apiContext.Messages.Where(x => !x.IsRead).ToListAsync();

        return Ok(_mapper.Map<List<ResultMessageDto>>(value));
    }
}
