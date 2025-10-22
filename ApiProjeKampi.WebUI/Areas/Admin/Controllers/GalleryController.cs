using ApiProjeKampi.WebUI.Constants.Area;
using ApiProjeKampi.WebUI.Dtos.ImageDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.Areas.Admin.Controllers;


[Area(AreaNames.Admin)]
public class GalleryController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public GalleryController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> ImageList()
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.GetAsync("https://localhost:7051/api/Images");
        if (responseMessage.IsSuccessStatusCode)
        {
            List<ResultImageDto>? data = await responseMessage.Content.ReadFromJsonAsync<List<ResultImageDto>>();
            return View(data);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateImage()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateImage(CreateImageDto createImageDto)
    {
        if (ModelState.IsValid)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync("https://localhost:7051/api/Images", createImageDto);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ImageList");
            }
        }
        return View(createImageDto);
    }

    public async Task<IActionResult> DeleteImage(int id)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.DeleteAsync($"https://localhost:7051/api/Images/{id}");

        return RedirectToAction("ImageList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateImage(int id)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.GetAsync($"https://localhost:7051/api/Images/GetById/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            GetImageByIdDto? data = await responseMessage.Content.ReadFromJsonAsync<GetImageByIdDto>();
            return View(data);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateImage(UpdateImageDto updateImageDto)
    {
        if (ModelState.IsValid)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            HttpResponseMessage responseMessage = await client.PutAsJsonAsync("https://localhost:7051/api/Images", updateImageDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ImageList");
            }
        }
        return View(updateImageDto);
    }
}