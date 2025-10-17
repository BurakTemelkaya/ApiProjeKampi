using ApiProjeKampi.WebAPI.Context;
using ApiProjeKampi.WebAPI.Dtos.ContactDtos;
using ApiProjeKampi.WebAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ApiProjeKampi.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly ApiContext _apiContext;

    public ContactsController(ApiContext apiContext)
    {
        _apiContext = apiContext;
    }

    [HttpGet]
    public async Task<IActionResult> ContactList(CancellationToken cancellationToken = default)
    {
        List<Contact> contacts = await _apiContext.Contacts.ToListAsync(cancellationToken);
        return Ok(contacts.Select(x=> new ResultContactDto()
        {
            ContactId = x.ContactId,
            Address = x.Address,
            Email = x.Email,
            MapLocation = x.MapLocation,
            Phone = x.Phone,
            WorkingHours = x.WorkingHours
        }));
    }

    [HttpGet("GetContact{id:int}")]
    public async Task<IActionResult> GetContactById(int id, CancellationToken cancellationToken = default)
    {
        Contact? contact = await _apiContext.Contacts.FindAsync(id, cancellationToken);
        if (contact == null)
        {
            return NotFound();
        }

        GetByIdContactDto getByIdContactDto = new()
        {
            ContactId = contact.ContactId,
            Address = contact.Address,
            Email = contact.Email,
            MapLocation = contact.MapLocation,
            Phone = contact.Phone,
            WorkingHours = contact.WorkingHours
        };

        return Ok(getByIdContactDto);
    }

    [HttpPost]
    public async Task<IActionResult> AddContact([FromBody] CreateContactDto createContactDto, CancellationToken cancellationToken = default)
    {
        Contact contact = new()
        {
            Address = createContactDto.Address,
            Email = createContactDto.Email,
            MapLocation = createContactDto.MapLocation,
            Phone = createContactDto.Phone,
            WorkingHours = createContactDto.WorkingHours

        };
        await _apiContext.Contacts.AddAsync(contact, cancellationToken);
        await _apiContext.SaveChangesAsync(cancellationToken);
        return Created(string.Empty, contact);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteContact(int id, CancellationToken cancellationToken = default)
    {
        Contact? contact = await _apiContext.Contacts.FindAsync(id, cancellationToken);
        if (contact == null)
        {
            return NotFound();
        }
        _apiContext.Contacts.Remove(contact);
        await _apiContext.SaveChangesAsync(cancellationToken);
        return Ok("Silme işlemi başarılı.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateContact([FromBody] UpdateContactDto updateContactDto, CancellationToken cancellationToken = default)
    {
        Contact? contact = await _apiContext.Contacts.FindAsync(updateContactDto.ContactId, cancellationToken);
        if (contact == null)
        {
            return NotFound();
        }
        contact.Address = updateContactDto.Address;
        contact.Email = updateContactDto.Email;
        contact.MapLocation = updateContactDto.MapLocation;
        contact.Phone = updateContactDto.Phone;
        contact.WorkingHours = updateContactDto.WorkingHours;
        _apiContext.Contacts.Update(contact);
        await _apiContext.SaveChangesAsync(cancellationToken);
        return Ok("Güncelleme işlemi başarılı.");
    }
}
