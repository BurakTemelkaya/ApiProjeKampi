using ApiProjeKampi.WebUI.Dtos.ImageDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.ViewComponents;

public class _DefaultGalleryComponentPartial:ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _DefaultGalleryComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
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
}