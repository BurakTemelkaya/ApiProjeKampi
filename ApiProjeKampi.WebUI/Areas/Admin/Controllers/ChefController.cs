using ApiProjeKampi.WebUI.Constants.Area;
using ApiProjeKampi.WebUI.Dtos.ChefDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.Areas.Admin.Controllers;


[Area(AreaNames.Admin)]
public class ChefController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ChefController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> ChefList()
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.GetAsync("https://localhost:7051/api/Chefs");
        if (responseMessage.IsSuccessStatusCode)
        {
            List<ResultChefDto>? data = await responseMessage.Content.ReadFromJsonAsync<List<ResultChefDto>>();
            return View(data);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateChef()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateChef(CreateChefDto createChefDto)
    {
        if (ModelState.IsValid)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync("https://localhost:7051/api/Chefs", createChefDto);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ChefList");
            }
        }
        return View(createChefDto);
    }

    public async Task<IActionResult> DeleteChef(int id)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.DeleteAsync($"https://localhost:7051/api/Chefs/{id}");

        return RedirectToAction("ChefList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateChef(int id)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.GetAsync($"https://localhost:7051/api/Chefs/GetChef/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            GetChefByIdDto? data = await responseMessage.Content.ReadFromJsonAsync<GetChefByIdDto>();
            return View(data);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateChef(UpdateChefDto updateChefDto)
    {
        if (ModelState.IsValid)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            HttpResponseMessage responseMessage = await client.PutAsJsonAsync("https://localhost:7051/api/Chefs", updateChefDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ChefList");
            }
        }
        return View(updateChefDto);
    }
}