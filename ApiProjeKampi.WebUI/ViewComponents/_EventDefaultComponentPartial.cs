using ApiProjeKampi.WebUI.Dtos.EventDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.ViewComponents;

public class _EventDefaultComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;
    public _EventDefaultComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        HttpClient httpClient = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await httpClient.GetAsync("https://localhost:7051/api/YummyEvents");
        if (responseMessage.IsSuccessStatusCode)
        {
            List<ResultEventDto>? data = await responseMessage.Content.ReadFromJsonAsync<List<ResultEventDto>>();
            return View(data);
        }
        return View();
    }
}