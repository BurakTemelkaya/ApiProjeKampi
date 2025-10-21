using ApiProjeKampi.WebUI.Constants.Area;
using ApiProjeKampi.WebUI.Dtos.CategoryDtos;
using ApiProjeKampi.WebUI.Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace ApiProjeKampi.WebUI.Areas.Admin.Controllers;

[Area(AreaNames.Admin)]
public class ProductController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProductController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> ProductList()
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.GetAsync("https://localhost:7051/api/Products");
        if (responseMessage.IsSuccessStatusCode)
        {
            List<ResultProductDto>? data = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductDto>>();
            return View(data);
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        HttpClient client = _httpClientFactory.CreateClient();

        HttpResponseMessage categoriesResponse = await client.GetAsync("https://localhost:7051/api/Categories");

        var categories = await categoriesResponse.Content.ReadFromJsonAsync<List<ResultCategoryDto>>();

        SelectList categorySelectList = new(categories, nameof(ResultCategoryDto.CategoryId), nameof(ResultCategoryDto.CategoryName));

        ViewBag.Categories = categorySelectList;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
    {
        if (ModelState.IsValid)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync("https://localhost:7051/api/Products/CreateProductWithCategory", createProductDto);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductList");
            }
        }
        return View(createProductDto);
    }

    public async Task<IActionResult> DeleteProduct(int id)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.DeleteAsync($"https://localhost:7051/api/Products/{id}");

        return RedirectToAction("ProductList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateProduct(int id)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMessage = await client.GetAsync($"https://localhost:7051/api/Products/GetProduct/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            GetProductByIdDto? data = await responseMessage.Content.ReadFromJsonAsync<GetProductByIdDto>();
            return View(data);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
    {
        if (ModelState.IsValid)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            HttpResponseMessage responseMessage = await client.PutAsJsonAsync("https://localhost:7051/api/Products", updateProductDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductList");
            }
        }
        return View(updateProductDto);
    }
}