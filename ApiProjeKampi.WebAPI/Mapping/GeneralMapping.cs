using ApiProjeKampi.WebAPI.Dtos.FeatureDtos;
using ApiProjeKampi.WebAPI.Dtos.MessageDtos;
using ApiProjeKampi.WebAPI.Dtos.ProductDtos;
using ApiProjeKampi.WebAPI.Entities;
using AutoMapper;

namespace ApiProjeKampi.WebAPI.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<Feature, ResultFeatureDto>().ReverseMap();
        CreateMap<Feature, CreateFeatureDto>().ReverseMap();
        CreateMap<Feature, UpdateFeatureDto>().ReverseMap();
        CreateMap<Feature, GetByIdFeatureDto>().ReverseMap();

        CreateMap<Message, ResultMessageDto>().ReverseMap();
        CreateMap<Message, CreateMessageDto>().ReverseMap();
        CreateMap<Message, UpdateMessageDto>().ReverseMap();
        CreateMap<Message, GetByIdMessageDto>().ReverseMap();

        CreateMap<Product, CreateProductDto>().ReverseMap();
        CreateMap<Product, ResultProductWithCategoryDto>()
            .ForMember(dest=> dest.CategoryName,member=> member.MapFrom(x=> x.Category.CategoryName))
            .ReverseMap();
    }
}