using ApiProjeKampi.WebUI.Constants.Area;
using ApiProjeKampi.WebUI.Dtos.ReservationDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.Areas.Admin.Controllers;

[Area(AreaNames.Admin)]
public class ReservationController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ReservationController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> ReservationList()
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.GetAsync("https://localhost:7051/api/Reservations");
        if (responseMessage.IsSuccessStatusCode)
        {
            List<ResultReservationDto>? data = await responseMessage.Content.ReadFromJsonAsync<List<ResultReservationDto>>();
            return View(data);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateReservation()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation(CreateReservationDto createReservationDto)
    {
        if (ModelState.IsValid)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync("https://localhost:7051/api/Reservations", createReservationDto);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ReservationList");
            }
        }
        return View(createReservationDto);
    }

    public async Task<IActionResult> DeleteReservation(int id)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.DeleteAsync($"https://localhost:7051/api/Reservations/{id}");

        return RedirectToAction("ReservationList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateReservation(int id)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.GetAsync($"https://localhost:7051/api/Reservations/GetById/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            GetReservationByIdDto? data = await responseMessage.Content.ReadFromJsonAsync<GetReservationByIdDto>();
            return View(data);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateReservation(UpdateReservationDto updateReservationDto)
    {
        if (ModelState.IsValid)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            HttpResponseMessage responseMessage = await client.PutAsJsonAsync("https://localhost:7051/api/Reservations", updateReservationDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ReservationList");
            }
        }
        return View(updateReservationDto);
    }
}
