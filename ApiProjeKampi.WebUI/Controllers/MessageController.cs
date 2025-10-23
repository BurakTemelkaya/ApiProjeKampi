using ApiProjeKampi.WebUI.Dtos.MessageDtos;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ApiProjeKampi.WebUI.Controllers;

public class MessageController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public MessageController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public PartialViewResult SendMessage()
    {
        return PartialView();
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(CreateMessageDto createMessageDto)
    {
        if (ModelState.IsValid)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            string apiKey = _configuration["HuggingFace"];

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentNullException("HuggingFace API Key Configuration dosyalarınızda bulunamadı.");
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            try
            {
                var translateRequestBody = new
                {
                    inputs = createMessageDto.MessageDetails,
                };

                HttpResponseMessage translateResponse = await client.PostAsJsonAsync("https://router.huggingface.co/hf-inference/models/Helsinki-NLP/opus-mt-tr-en", translateRequestBody);

                var translateResponseString = await translateResponse.Content.ReadAsStringAsync();
                    
                string englishText = createMessageDto.MessageDetails;

                if (translateResponseString.TrimStart().StartsWith("["))
                {
                    var translateDoc = JsonDocument.Parse(translateResponseString);
                    englishText = translateDoc.RootElement[0].GetProperty("translation_text").GetString();
                }

                var toxicityRequestBody = new
                {
                    inputs = englishText,
                };

                HttpResponseMessage toxicityResponse = await client.PostAsJsonAsync("https://router.huggingface.co/hf-inference/models/unitary/toxic-bert", toxicityRequestBody);

                var toxicityResponseString = await toxicityResponse.Content.ReadAsStringAsync();

                if (toxicityResponseString.TrimStart().StartsWith("["))
                {
                    var toxicDocument = JsonDocument.Parse(toxicityResponseString);
                    foreach (var item in toxicDocument.RootElement[0].EnumerateArray())
                    {
                        string label = item.GetProperty("label").GetString();
                        double score = item.GetProperty("score").GetDouble();

                        if (score > 0.5)
                        {
                            createMessageDto.Status = "Toxic Message";
                            break;
                        }
                    }
                }
                if (string.IsNullOrEmpty(createMessageDto.Status))
                {
                    createMessageDto.Status = "Mesaj Alındı";
                }
            }
            catch (Exception ex)
            {
                createMessageDto.Status = "Onay Bekliyor";
            }

            HttpClient client2 = _httpClientFactory.CreateClient();

            HttpResponseMessage responseMessage = await client2.PostAsJsonAsync("https://localhost:7051/api/Messages", createMessageDto);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(DefaultController.Index), "Default");
            }
        }
        return BadRequest("Mesaj gönderimi sırasında bir hata oluştu lütfen daha sonra tekrar deneyiniz.");
    }   
}