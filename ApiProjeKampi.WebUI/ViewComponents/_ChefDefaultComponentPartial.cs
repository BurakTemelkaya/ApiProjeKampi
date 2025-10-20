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
    public async Task<IViewComponentResult> InvokeAsync()
    {
        HttpClient httpClient = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await httpClient.GetAsync("https://localhost:7051/api/Chefs");
        if (responseMessage.IsSuccessStatusCode)
        {
            List<ResultChefDto>? data = await responseMessage.Content.ReadFromJsonAsync<List<ResultChefDto>>();
            return View(data);
        }
        return View();
    }
}