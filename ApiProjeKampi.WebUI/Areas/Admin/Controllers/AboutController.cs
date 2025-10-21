using ApiProjeKampi.WebUI.Constants.Area;
using ApiProjeKampi.WebUI.Dtos.AboutDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.Areas.Admin.Controllers;

[Area(AreaNames.Admin)]
public class AboutController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AboutController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> AboutList()
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.GetAsync("https://localhost:7051/api/Abouts");
        if (responseMessage.IsSuccessStatusCode)
        {
            List<ResultAboutDto>? data = await responseMessage.Content.ReadFromJsonAsync<List<ResultAboutDto>>();
            return View(data);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateAbout()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
    {
        if (ModelState.IsValid)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync("https://localhost:7051/api/Abouts", createAboutDto);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("AboutList");
            }
        }
        return View(createAboutDto);
    }

    public async Task<IActionResult> DeleteAbout(int id)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.DeleteAsync($"https://localhost:7051/api/Abouts/{id}");

        return RedirectToAction("AboutList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateAbout(int id)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.GetAsync($"https://localhost:7051/api/Abouts/GetById/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            GetAboutByIdDto? data = await responseMessage.Content.ReadFromJsonAsync<GetAboutByIdDto>();
            return View(data);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
    {
        if (ModelState.IsValid)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            HttpResponseMessage responseMessage = await client.PutAsJsonAsync("https://localhost:7051/api/Abouts", updateAboutDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("AboutList");
            }
        }
        return View(updateAboutDto);
    }
}