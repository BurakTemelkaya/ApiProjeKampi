using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.ViewComponents.HomePageViewComponents;

public class _HomePageStatisticsComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _HomePageStatisticsComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        HttpClient client = _httpClientFactory.CreateClient();

        client.BaseAddress = new Uri("https://localhost:7051/api/Statistics/");

        ViewBag.ProductCount = await client.GetFromJsonAsync<int>("ProductCount");

        ViewBag.ReservationCount = await client.GetFromJsonAsync<int>("ReservationCount");

        ViewBag.ChefCount = await client.GetFromJsonAsync<int>("ChefCount");

        ViewBag.TotalGuestCount = await client.GetFromJsonAsync<int>("TotalGuestCount");

        return View();
    }
}