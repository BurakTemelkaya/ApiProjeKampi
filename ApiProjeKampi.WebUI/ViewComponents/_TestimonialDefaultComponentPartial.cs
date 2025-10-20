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

    public async Task<IViewComponentResult> InvokeAsync()
    {
        HttpClient httpClient = _httpClientFactory.CreateClient();

        HttpResponseMessage responseMessage = await httpClient.GetAsync("https://localhost:7051/api/Testimonials");

        if (responseMessage.IsSuccessStatusCode)
        {
            List<ResultTestimonialDto>? data = await responseMessage.Content.ReadFromJsonAsync<List<ResultTestimonialDto>>();

            return View(data);
        }
        return View();
    }
}
