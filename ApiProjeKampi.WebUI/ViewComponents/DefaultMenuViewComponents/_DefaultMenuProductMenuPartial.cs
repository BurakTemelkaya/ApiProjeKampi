using ApiProjeKampi.WebUI.Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.ViewComponents.DefaultMenuViewComponents;

public class _DefaultMenuProductMenuPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _DefaultMenuProductMenuPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        HttpClient httpClient = _httpClientFactory.CreateClient();

        HttpResponseMessage responseMessage = await httpClient.GetAsync("https://localhost:7051/api/Products");

        if (responseMessage.IsSuccessStatusCode)
        {
            List<ResultProductDto>? data = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductDto>>();

            return View(data);
        }
        return View();
    }
}