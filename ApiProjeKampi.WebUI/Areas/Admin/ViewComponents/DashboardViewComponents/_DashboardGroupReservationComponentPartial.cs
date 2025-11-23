using ApiProjeKampi.WebUI.Dtos.GroupReservationDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.Areas.Admin.ViewComponents.DashboardViewComponents;

public class _DashboardGroupReservationComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _DashboardGroupReservationComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken = default)
    {
        HttpClient client = _httpClientFactory.CreateClient();

        List<ResultGroupReservationDto>? result = await client.GetFromJsonAsync<List<ResultGroupReservationDto>>("https://localhost:7051/api/GroupReservations", cancellationToken);

        return View(result);
    }
}