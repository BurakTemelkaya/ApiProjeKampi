using ApiProjeKampi.WebUI.Constants.Area;
using ApiProjeKampi.WebUI.Dtos.ServiceDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.Areas.Admin.Controllers;

[Area(AreaNames.Admin)]
public class WhyChooseYummyController : Controller
{

    private readonly IHttpClientFactory _httpClientFactory;

    public WhyChooseYummyController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> WhyChooseYummyList()
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.GetAsync("https://localhost:7051/api/Services");
        if (responseMessage.IsSuccessStatusCode)
        {
            List<ResultServiceDto>? data = await responseMessage.Content.ReadFromJsonAsync<List<ResultServiceDto>>();
            return View(data);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateWhyChooseYummy()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateWhyChooseYummy(CreateServiceDto createServiceDto)
    {
        if (ModelState.IsValid)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync("https://localhost:7051/api/Services", createServiceDto);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(WhyChooseYummyList));
            }
        }
        return View(createServiceDto);
    }

    public async Task<IActionResult> DeleteWhyChooseYummy(int id)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.DeleteAsync($"https://localhost:7051/api/Services/{id}");

        return RedirectToAction(nameof(WhyChooseYummyList));
    }

    [HttpGet]
    public async Task<IActionResult> UpdateWhyChooseYummy(int id)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.GetAsync($"https://localhost:7051/api/Services/GetById/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            GetServiceByIdDto? data = await responseMessage.Content.ReadFromJsonAsync<GetServiceByIdDto>();
            return View(data);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateWhyChooseYummy(UpdateServiceDto updateServiceDto)
    {
        if (ModelState.IsValid)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            HttpResponseMessage responseMessage = await client.PutAsJsonAsync("https://localhost:7051/api/Services", updateServiceDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(WhyChooseYummyList));
            }
        }
        return View(updateServiceDto);
    }
}