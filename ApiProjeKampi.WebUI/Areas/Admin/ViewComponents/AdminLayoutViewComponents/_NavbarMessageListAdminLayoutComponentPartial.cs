using ApiProjeKampi.WebUI.Dtos.MessageDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.Areas.Admin.ViewComponents.AdminLayoutViewComponents;

public class _NavbarMessageListAdminLayoutComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;
    public _NavbarMessageListAdminLayoutComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        HttpClient httpClient = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await httpClient.GetAsync("https://localhost:7051/api/Messages/MessageListByUnreadMessages");
        if (responseMessage.IsSuccessStatusCode)
        {
            List<ResultMessageByUnreadDto>? data = await responseMessage.Content.ReadFromJsonAsync<List<ResultMessageByUnreadDto>>();
            return View(data);
        }
        return View();
    }
}