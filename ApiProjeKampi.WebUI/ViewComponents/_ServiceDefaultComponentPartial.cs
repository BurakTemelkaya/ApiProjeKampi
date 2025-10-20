using ApiProjeKampi.WebUI.Dtos.ServiceDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.ViewComponents;

public class _ServiceDefaultComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _ServiceDefaultComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        HttpClient client = _httpClientFactory.CreateClient();

        HttpResponseMessage responseMessage = await client.GetAsync("https://localhost:7051/api/Services");

        if (responseMessage.IsSuccessStatusCode)
        {
            List<ResultServiceDto>? values = await responseMessage.Content.ReadFromJsonAsync<List<ResultServiceDto>>();
            return View(values);
        }
        return View();
    }
}