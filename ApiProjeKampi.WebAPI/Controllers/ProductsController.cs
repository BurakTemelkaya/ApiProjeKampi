using ApiProjeKampi.WebAPI.Context;
using ApiProjeKampi.WebAPI.Dtos.ProductDtos;
using ApiProjeKampi.WebAPI.Entities;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ApiProjeKampi.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IValidator<Product> _validator;
    private readonly ApiContext _apiContext;
    private readonly IMapper _mapper;

    public ProductsController(IValidator<Product> validator, ApiContext apiContext, IMapper mapper)
    {
        _validator = validator;
        _apiContext = apiContext;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> ProductList(CancellationToken cancellationToken = default)
    {
        List<Product> products = await _apiContext.Products.ToListAsync(cancellationToken);
        return Ok(products);
    }

    [HttpGet("GetProduct{id:int}")]
    public async Task<IActionResult> GetProduct(int id, CancellationToken cancellationToken = default)
    {
        Product? product = await _apiContext.Products.FindAsync(id, cancellationToken);
        if (product == null)
        {
            return NotFound("Ürün bulunamadı.");
        }
        return Ok(product);
    }


    [HttpPost]
    public async Task<IActionResult> CreateProduct(Product product, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(product, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
        }

        await _apiContext.Products.AddAsync(product, cancellationToken);
        await _apiContext.SaveChangesAsync(cancellationToken);
        return Created(string.Empty, new { message = "Ürün ekleme işlemi başarılı.", data = product });
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(Product product, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(product, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
        }
        _apiContext.Products.Update(product);
        await _apiContext.SaveChangesAsync(cancellationToken);
        return Ok("Ürün güncelleme işlemi başarılı.");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(int id, CancellationToken cancellationToken = default)
    {
        var product = await _apiContext.Products.FindAsync(id, cancellationToken);
        if (product == null)
        {
            return NotFound("Ürün bulunamadı.");
        }
        _apiContext.Products.Remove(product);
        await _apiContext.SaveChangesAsync(cancellationToken);
        return Ok("Ürün silme işlemi başarılı.");
    }

    [HttpPost("CreateProductWithCategory")]
    public async Task<IActionResult> CreateProductWithCategory(CreateProductDto createProductDto, CancellationToken cancellationToken = default)
    {
        var product = _mapper.Map<Product>(createProductDto);

        await _apiContext.Products.AddAsync(product, cancellationToken);
        await _apiContext.SaveChangesAsync(cancellationToken);
        return Created(string.Empty, new { message = "Ürün işlemi başarılı.", data = product });
    }

    [HttpGet("ProductListWithCategory")]
    public async Task<IActionResult> ProductListWithCategory(CancellationToken cancellationToken = default)
    {
        List<Product> products = await _apiContext.Products
            .Include(p => p.Category)
            .ToListAsync(cancellationToken);
        return Ok(_mapper.Map<List<ResultProductWithCategoryDto>>(products));
    }

}