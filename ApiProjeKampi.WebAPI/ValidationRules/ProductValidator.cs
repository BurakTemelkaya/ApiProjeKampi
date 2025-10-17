using ApiProjeKampi.WebAPI.Entities;
using FluentValidation;

namespace ApiProjeKampi.WebAPI.ValidationRules;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.ProductName).NotEmpty().WithMessage("Ürün adını boş geçmeyin.");
        RuleFor(p => p.ProductName).MinimumLength(2).WithMessage("Ürün adı için en az 2 karekter veri yapın.");
        RuleFor(p => p.ProductName).MaximumLength(50).WithMessage("Ürün adı için en fazla 50 karekter veri yapın.");

        RuleFor(p=> p.Price).NotEmpty().WithMessage("Ürün fiyatını boş geçmeyin.")
            .GreaterThan(0).WithMessage("Ürün fiyatı negatif olamaz")
            .LessThan(1000).WithMessage("Ürün fiyatı bu kadar yüksek olamaz. Girdiğiniz değeri kontrol edin.");

        RuleFor(p=> p.ProductDescription).NotEmpty().WithMessage("Ürün açıklamasını boş geçmeyin.");
    }
}
