using ApiProjeKampi.WebUI.Dtos.AISuggestionsDtos;
using ApiProjeKampi.WebUI.Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiProjeKampi.WebUI.Areas.Admin.ViewComponents.DashboardViewComponents;

public class _DashboardAIDailyMenuSuggestionComponentPartial : ViewComponent
{
    private readonly string _openRouterApiKey = string.Empty;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public _DashboardAIDailyMenuSuggestionComponentPartial(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _openRouterApiKey = _configuration["OpenRouterKey"];
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var openAiClient = _httpClientFactory.CreateClient();
        openAiClient.BaseAddress = new Uri("https://openrouter.ai/api/");
        openAiClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _openRouterApiKey);

        string prompt = @"
4 farklı dünya mutfağından rastgele günlük menü üret.

Kurallar:
- Mutlaka 4 farklı ülke mutfağı seç.
- Tüm içerik TÜRKÇE olsun.
- Her ülke için 4 yemek yaz (çorba, ana yemek, yan yemek, tatlı).
- Yemek açıklamaları Türkçe olsun.
- Fiyatları TL cinsinden ver.
- Ülke adını Türkçe yaz (İtalyan Mutfağı, Çin Mutfağı gibi).
- Ayrıca her mutfağa ISO ülke kodunu ekle (IT, CN, TR, JP, FR vs.)
- Cevap SADECE geçerli JSON olsun.

JSON formatı TAM OLARAK şöyle olsun:

[
  {
    ""Cuisine"": ""İtalyan Mutfağı"",
    ""CountryCode"": ""IT"",
    ""MenuTitle"": ""Geleneksel İtalyan Günlük Menüsü"",
    ""Items"": [
      { ""Name"": ""Minestrone Çorbası"", ""Description"": ""Sebzeli geleneksel çorba"", ""Price"": 85 },
      { ""Name"": ""Spaghetti Carbonara"", ""Description"": ""Özel soslu makarna"", ""Price"": 180 },
      { ""Name"": ""Sarımsaklı Ekmek"", ""Description"": ""Tereyağlı kızarmış ekmek"", ""Price"": 45 },
      { ""Name"": ""Tiramisu"", ""Description"": ""Kakao aromalı tatlı"", ""Price"": 70 }
    ]
  }
]
";

        var body = new
        {
            model = "openai/gpt-oss-20b:free",   // istersen değiştir
            messages = new[]
            {
                new { role = "system", content = "Sadece JSON üret." },
                new { role = "user", content = prompt }
            }
        };

        var jsonBody = JsonConvert.SerializeObject(body);
        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        var response = await openAiClient.PostAsync("v1/chat/completions", content);
        var responseJson = await response.Content.ReadAsStringAsync();

        dynamic obj = JsonConvert.DeserializeObject(responseJson);
        string aiContent = obj.choices[0].message.content.ToString();

        List<MenuSuggestionDto> menus;

        try
        {
            menus = JsonConvert.DeserializeObject<List<MenuSuggestionDto>>(aiContent);
        }
        catch
        {
            menus = new();
        }

        return View(menus);
    }

    public async Task<List<ResultProductDto>> GetProductsAsync()
    {
        HttpClient client = _httpClientFactory.CreateClient();

        HttpResponseMessage response = await client.GetAsync("https://localhost:7231/api/products/ProductListWithCategory");

        if (response.IsSuccessStatusCode)
        {
            List<ResultProductDto>? products = await response.Content.ReadFromJsonAsync<List<ResultProductDto>>();
            return products ?? [];
        }

        return [];
    }
}