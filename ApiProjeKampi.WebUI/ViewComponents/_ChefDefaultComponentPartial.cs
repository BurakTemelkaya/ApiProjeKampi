using ApiProjeKampi.WebUI.Dtos.ChefDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.ViewComponents;

public class _ChefDefaultComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;
    public _ChefDefaultComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken = default)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await httpClient.GetAsync("https://localhost:7051/api/Chefs", cancellationToken);
        if (responseMessage.IsSuccessStatusCode)
        {
            List<ResultChefDto>? data = await responseMessage.Content.ReadFromJsonAsync<List<ResultChefDto>>(cancellationToken);
            return View(data);
        }
        return View();
    }
}