using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.ViewComponents.HomePageViewComponents;

public class _HomePageStatisticsComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _HomePageStatisticsComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken = default)
    {
        HttpClient client = _httpClientFactory.CreateClient();

        client.BaseAddress = new Uri("https://localhost:7051/api/Statistics/");

        ViewBag.ProductCount = await client.GetFromJsonAsync<int>("ProductCount", cancellationToken);

        ViewBag.ReservationCount = await client.GetFromJsonAsync<int>("ReservationCount", cancellationToken);

        ViewBag.ChefCount = await client.GetFromJsonAsync<int>("ChefCount", cancellationToken);

        ViewBag.TotalGuestCount = await client.GetFromJsonAsync<int>("TotalGuestCount", cancellationToken);

        return View();
    }
}