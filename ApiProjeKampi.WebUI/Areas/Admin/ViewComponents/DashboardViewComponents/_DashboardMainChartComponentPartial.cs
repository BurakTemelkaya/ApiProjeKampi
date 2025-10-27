using ApiProjeKampi.WebUI.Dtos.ReservationDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.Areas.Admin.ViewComponents.DashboardViewComponents;

public class _DashboardMainChartComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _DashboardMainChartComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken = default)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri("https://localhost:7051");

        List<ReservationChartDto>? response = await client.GetFromJsonAsync<List<ReservationChartDto>>("api/Reservations/GetReservationStats", cancellationToken);

        return View(response);
    }
}