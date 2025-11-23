using ApiProjeKampi.WebUI.Dtos.ContactDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.ViewComponents;

public class _DefaultContactComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _DefaultContactComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken)
    {
        HttpClient client = _httpClientFactory.CreateClient();

        List<ResultContactDto>? contactDtos = await client.GetFromJsonAsync<List<ResultContactDto>>("https://localhost:7051/api/Contacts", cancellationToken);

        return View(contactDtos);
    }
}