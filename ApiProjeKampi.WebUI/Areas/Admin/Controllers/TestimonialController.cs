using ApiProjeKampi.WebUI.Constants.Area;
using ApiProjeKampi.WebUI.Dtos.TestimonialDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.Areas.Admin.Controllers;

[Area(AreaNames.Admin)]
public class TestimonialController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public TestimonialController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> TestimonialList()
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.GetAsync("https://localhost:7051/api/Testimonials");
        if (responseMessage.IsSuccessStatusCode)
        {
            List<ResultTestimonialDto>? data = await responseMessage.Content.ReadFromJsonAsync<List<ResultTestimonialDto>>();
            return View(data);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateTestimonial()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto createTestimonialDto)
    {
        if (ModelState.IsValid)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync("https://localhost:7051/api/Testimonials", createTestimonialDto);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("TestimonialList");
            }
        }
        return View(createTestimonialDto);
    }

    public async Task<IActionResult> DeleteTestimonial(int id)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.DeleteAsync($"https://localhost:7051/api/Testimonials/{id}");

        return RedirectToAction("TestimonialList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateTestimonial(int id)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.GetAsync($"https://localhost:7051/api/Testimonials/GetById/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            GetTestimonialByIdDto? data = await responseMessage.Content.ReadFromJsonAsync<GetTestimonialByIdDto>();
            return View(data);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialDto updateTestimonialDto)
    {
        if (ModelState.IsValid)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            HttpResponseMessage responseMessage = await client.PutAsJsonAsync("https://localhost:7051/api/Testimonials", updateTestimonialDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("TestimonialList");
            }
        }
        return View(updateTestimonialDto);
    }
}