using ApiProjeKampi.WebUI.Constants.Area;
using ApiProjeKampi.WebUI.Dtos.ContactDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.Areas.Admin.Controllers;


[Area(AreaNames.Admin)]
public class ContactController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ContactController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> ContactList()
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.GetAsync("https://localhost:7051/api/Contacts");
        if (responseMessage.IsSuccessStatusCode)
        {
            List<ResultContactDto>? data = await responseMessage.Content.ReadFromJsonAsync<List<ResultContactDto>>();
            return View(data);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateContact()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
    {
        if (ModelState.IsValid)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync("https://localhost:7051/api/Contacts", createContactDto);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ContactList");
            }
        }
        return View(createContactDto);
    }

    public async Task<IActionResult> DeleteContact(int id)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.DeleteAsync($"https://localhost:7051/api/Contacts/{id}");

        return RedirectToAction("ContactList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateContact(int id)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.GetAsync($"https://localhost:7051/api/Contacts/GetContact/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            GetContactByIdDto? data = await responseMessage.Content.ReadFromJsonAsync<GetContactByIdDto>();
            return View(data);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
    {
        if (ModelState.IsValid)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            HttpResponseMessage responseMessage = await client.PutAsJsonAsync("https://localhost:7051/api/Contacts", updateContactDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ContactList");
            }
        }
        return View(updateContactDto);
    }
}