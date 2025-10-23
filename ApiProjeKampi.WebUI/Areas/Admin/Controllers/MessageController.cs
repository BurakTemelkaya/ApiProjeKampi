using ApiProjeKampi.WebUI.Constants.Area;
using ApiProjeKampi.WebUI.Dtos.MessageDtos;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using static ApiProjeKampi.WebUI.Areas.Admin.Controllers.AIController;

namespace ApiProjeKampi.WebUI.Areas.Admin.Controllers;


[Area(AreaNames.Admin)]
public class MessageController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public MessageController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
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

    [HttpGet]
    public async Task<IActionResult> MessageAnswerWithOpenAI(int id, string prompt)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.GetAsync($"https://localhost:7051/api/Messages/GetMessage/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            GetMessageByIdDto? data = await responseMessage.Content.ReadFromJsonAsync<GetMessageByIdDto>();

            string apiKey = _configuration["OpenRouterKey"];

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentNullException("OpenRouterKey değeri configuration dosyasında bulunamadı.");
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            prompt = data.MessageDetails;

            var requestData = new
            {
                model = "openai/gpt-oss-20b:free",
                messages = new[]
                {
                new { role = "system", content = "Sen bir restoran için kullanıcıların göndermiş oldukları mesajları detaylı ve olabildiğince olumlu müşteri memnuniyeti gözeten cevaplar veren bir yapay zeka aracısın. Amacımız kullanıcı tarafından gönderilen mesajlara en olumlu ve en mantıklı cevapları sunabilmek." },
                new { role = "user", content = prompt }
            }
            };

            HttpResponseMessage response = await client.PostAsJsonAsync("https://openrouter.ai/api/v1/chat/completions", requestData);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<OpenAIResponse>();
                var content = result.Choices[0].Message.Content;
                ViewBag.AnswerAI = content;
            }
            else
            {
                ViewBag.AnswerAI = "Yapay zeka ile iletişimde bir hata oluştu." + await response.Content.ReadAsStringAsync();
            }

            return View(data);
        }
        return View();
    }
}