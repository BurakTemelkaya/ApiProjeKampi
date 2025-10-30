using ApiProjeKampi.WebUI.Dtos.FeatureDtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiProjeKampi.WebUI.ViewComponents;

public class _FeatureDefaultComponentPartial:ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _FeatureDefaultComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken = default)
    {
        HttpClient client = _httpClientFactory.CreateClient();

        List<ResultFeatureDto>? features = await client.GetFromJsonAsync<List<ResultFeatureDto>>("https://localhost:7051/api/Features", cancellationToken);

        return View(features);
    }
}
