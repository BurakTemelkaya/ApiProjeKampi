using ApiProjeKampi.WebUI.Constants.Area;
using ApiProjeKampi.WebUI.Dtos.MessageDtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.Areas.Admin.Controllers;


[Area(AreaNames.Admin)]
public class MessageController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public MessageController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> MessageList()
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.GetAsync("https://localhost:7051/api/Messages");
        if (responseMessage.IsSuccessStatusCode)
        {
            List<ResultMessageDto>? data = await responseMessage.Content.ReadFromJsonAsync<List<ResultMessageDto>>();
            return View(data);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateMessage()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateMessage(CreateMessageDto createMessageDto)
    {
        if (ModelState.IsValid)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync("https://localhost:7051/api/Messages", createMessageDto);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("MessageList");
            }
        }
        return View(createMessageDto);
    }

    public async Task<IActionResult> DeleteMessage(int id)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.DeleteAsync($"https://localhost:7051/api/Messages/{id}");

        return RedirectToAction("MessageList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateMessage(int id)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.GetAsync($"https://localhost:7051/api/Messages/GetMessage/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            GetMessageByIdDto? data = await responseMessage.Content.ReadFromJsonAsync<GetMessageByIdDto>();
            return View(data);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateMessage(UpdateMessageDto updateMessageDto)
    {
        if (ModelState.IsValid)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            HttpResponseMessage responseMessage = await client.PutAsJsonAsync("https://localhost:7051/api/Messages", updateMessageDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("MessageList");
            }
        }
        return View(updateMessageDto);
    }
}