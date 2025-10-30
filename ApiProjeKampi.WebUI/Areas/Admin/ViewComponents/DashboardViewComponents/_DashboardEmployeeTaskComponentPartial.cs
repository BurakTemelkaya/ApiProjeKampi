using ApiProjeKampi.WebUI.Dtos.EmployeeTaskDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.Areas.Admin.ViewComponents.DashboardViewComponents;

public class _DashboardEmployeeTaskComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _DashboardEmployeeTaskComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken = default)
    {
        HttpClient client = _httpClientFactory.CreateClient();

        List<ResultEmployeeTaskDto>? result = await client.GetFromJsonAsync<List<ResultEmployeeTaskDto>>("https://localhost:7051/api/EmployeeTasks", cancellationToken);

        return View(result);
    }
}