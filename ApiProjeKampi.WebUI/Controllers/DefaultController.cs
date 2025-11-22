using ApiProjeKampi.WebUI.Dtos.ReservationDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.Controllers;

public class DefaultController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public DefaultController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(CreateReservationDto createReservationDto)
    {        
        createReservationDto.ReservationStatus = "Onay Bekliyor";

        HttpClient client = _httpClientFactory.CreateClient();

        HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7051/api/Reservations", createReservationDto);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));            
        }

        return View(createReservationDto);
    }
}