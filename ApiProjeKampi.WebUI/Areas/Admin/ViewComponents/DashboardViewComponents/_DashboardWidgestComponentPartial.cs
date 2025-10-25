using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiProjeKampi.WebUI.Areas.Admin.ViewComponents.DashboardViewComponents;

public class _DashboardWidgestComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _DashboardWidgestComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        HttpClient httpClient = _httpClientFactory.CreateClient();

        HttpResponseMessage totalReservationResponse = await httpClient.GetAsync("https://localhost:7051/api/Reservations/GetTotalReservationCount");

        ViewBag.TotalReservationCount = await totalReservationResponse.Content.ReadAsStringAsync();

        HttpResponseMessage totalCustomerResponse = await httpClient.GetAsync("https://localhost:7051/api/Reservations/GetTotalCustomerCount");

        ViewBag.TotalCustomerCount = await totalCustomerResponse.Content.ReadAsStringAsync();

        HttpResponseMessage totalPendingResponse = await httpClient.GetAsync("https://localhost:7051/api/Reservations/GetPendingReservation");

        ViewBag.TotalPendingCount = await totalPendingResponse.Content.ReadAsStringAsync();

        HttpResponseMessage totalApprovedResponse = await httpClient.GetAsync("https://localhost:7051/api/Reservations/GetApprovedReservation");

        ViewBag.TotalApprovedCount = await totalApprovedResponse.Content.ReadAsStringAsync();

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
