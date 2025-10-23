using ApiProjeKampi.WebUI.Constants.Area;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace ApiProjeKampi.WebUI.Areas.Admin.Controllers;


[Area(AreaNames.Admin)]
public class AIController : Controller
{
    private readonly IConfiguration _configuration;

    public AIController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult CreateRecipeWithOpenAI()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateRecipeWithOpenAI(string prompt)
    {
        string apiKey = _configuration["OpenRouterKey"];

        if (string.IsNullOrEmpty(apiKey))
        {
            throw new ArgumentNullException("OpenRouterKey değeri configuration dosyasında bulunamadı.");
        }

        using var client = new HttpClient();

        client.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Bearer", apiKey);

        var requestData = new
        {
            model = "openai/gpt-oss-20b:free",
            messages = new[]
            {
                new { role = "system", content = "Sen bir restoran için yemek önerileri yapan bir yapay zeka aracısın. Amacımız kullanıcı tarafından girilen malzemelere göre yemek tarifi önerisinde bulunmak. Sonucu her zaman HTML olarak geri ver." },
                new { role = "user", content = prompt }
            }
        };

        HttpResponseMessage response = await client.PostAsJsonAsync("https://openrouter.ai/api/v1/chat/completions", requestData);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<OpenAIResponse>();
            var content = result.Choices[0].Message.Content;
            ViewBag.Recipe = content;
        }
        else
        {
            ViewBag.Recipe = "Yapay zeka ile iletişimde bir hata oluştu." + await response.Content.ReadAsStringAsync();
        }

        return View();
    }

    public class OpenAIResponse
    {
        public List<Choice> Choices { get; set; }
    }

    public class Choice
    {
        public Message Message { get; set; }
    }

    public class Message
    {
        public string Role { get; set; }
        public string Content { get; set; }
    }
}
