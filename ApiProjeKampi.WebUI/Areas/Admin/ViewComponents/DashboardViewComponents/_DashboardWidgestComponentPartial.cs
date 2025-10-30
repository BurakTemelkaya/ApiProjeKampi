using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.Areas.Admin.ViewComponents.DashboardViewComponents;

public class _DashboardWidgestComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _DashboardWidgestComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken = default)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient();

        HttpResponseMessage totalReservationResponse = await httpClient.GetAsync("https://localhost:7051/api/Reservations/GetTotalReservationCount", cancellationToken);

        ViewBag.TotalReservationCount = await totalReservationResponse.Content.ReadAsStringAsync(cancellationToken);

        HttpResponseMessage totalCustomerResponse = await httpClient.GetAsync("https://localhost:7051/api/Reservations/GetTotalCustomerCount", cancellationToken);

        ViewBag.TotalCustomerCount = await totalCustomerResponse.Content.ReadAsStringAsync(cancellationToken);

        HttpResponseMessage totalPendingResponse = await httpClient.GetAsync("https://localhost:7051/api/Reservations/GetPendingReservation", cancellationToken);

        ViewBag.TotalPendingCount = await totalPendingResponse.Content.ReadAsStringAsync(cancellationToken);

        HttpResponseMessage totalApprovedResponse = await httpClient.GetAsync("https://localhost:7051/api/Reservations/GetApprovedReservation", cancellationToken);

        ViewBag.TotalApprovedCount = await totalApprovedResponse.Content.ReadAsStringAsync(cancellationToken);

        int r1, r2, r3, r4;

        Random rnd = new Random();

        r1 = rnd.Next(1, 100);
        r2 = rnd.Next(1, 100);
        r3 = rnd.Next(1, 100);
        r4 = rnd.Next(1, 100);

        ViewBag.R1 = r1;
        ViewBag.R2 = r2;
        ViewBag.R3 = r3;
        ViewBag.R4 = r4;

        return View();
    }
}
