using ApiProjeKampi.WebUI.Constants.Area;
using ApiProjeKampi.WebUI.Dtos.YummyEventDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.Areas.Admin.Controllers;


[Area(AreaNames.Admin)]
public class YummyEventController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public YummyEventController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> YummyEventList()
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.GetAsync("https://localhost:7051/api/YummyEvents");
        if (responseMessage.IsSuccessStatusCode)
        {
            List<ResultYummyEventDto>? data = await responseMessage.Content.ReadFromJsonAsync<List<ResultYummyEventDto>>();
            return View(data);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateYummyEvent()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateYummyEvent(CreateYummyEventDto createYummyEventDto)
    {
        if (ModelState.IsValid)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync("https://localhost:7051/api/YummyEvents", createYummyEventDto);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("YummyEventList");
            }
        }
        return View(createYummyEventDto);
    }

    public async Task<IActionResult> DeleteYummyEvent(int id)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.DeleteAsync($"https://localhost:7051/api/YummyEvents/{id}");

        return RedirectToAction("YummyEventList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateYummyEvent(int id)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.GetAsync($"https://localhost:7051/api/YummyEvents/GetYummyEvent/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            GetYummyEventByIdDto? data = await responseMessage.Content.ReadFromJsonAsync<GetYummyEventByIdDto>();
            return View(data);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateYummyEvent(UpdateYummyEventDto updateYummyEventDto)
    {
        if (ModelState.IsValid)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            HttpResponseMessage responseMessage = await client.PutAsJsonAsync("https://localhost:7051/api/YummyEvents", updateYummyEventDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("YummyEventList");
            }
        }
        return View(updateYummyEventDto);
    }
}