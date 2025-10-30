using ApiProjeKampi.WebUI.Dtos.TestimonialDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.ViewComponents;

public class _TestimonialDefaultComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _TestimonialDefaultComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken = default)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient();

        HttpResponseMessage responseMessage = await httpClient.GetAsync("https://localhost:7051/api/Testimonials",cancellationToken);

        if (responseMessage.IsSuccessStatusCode)
        {
            List<ResultTestimonialDto>? data = await responseMessage.Content.ReadFromJsonAsync<List<ResultTestimonialDto>>(cancellationToken);

            return View(data);
        }
        return View();
    }
}
