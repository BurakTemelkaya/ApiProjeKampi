using ApiProjeKampi.WebUI.Dtos.CategoryDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.ViewComponents.DefaultMenuViewComponents;

public class _DefaultMenuCategoryComponentPartial:ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _DefaultMenuCategoryComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        HttpClient httpClient = _httpClientFactory.CreateClient();

        HttpResponseMessage responseMessage = await httpClient.GetAsync("https://localhost:7051/api/Categories");

        if (responseMessage.IsSuccessStatusCode)
        {
            List<ResultCategoryDto>? data = await responseMessage.Content.ReadFromJsonAsync<List<ResultCategoryDto>>();

            return View(data);
        }
        return View();
    }
}