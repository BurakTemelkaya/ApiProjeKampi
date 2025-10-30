using ApiProjeKampi.WebUI.Dtos.NotificationDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.Areas.Admin.ViewComponents.AdminLayoutViewComponents;

public class _NavbarNotificationAdminLayoutComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;
    public _NavbarNotificationAdminLayoutComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken = default)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await httpClient.GetAsync("https://localhost:7051/api/Notifications", cancellationToken);
        if (responseMessage.IsSuccessStatusCode)
        {
            List<ResultNotificationDto>? data = await responseMessage.Content.ReadFromJsonAsync<List<ResultNotificationDto>>(cancellationToken);
            return View(data);
        }
        return View();
    }
}