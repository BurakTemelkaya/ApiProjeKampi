using ApiProjeKampi.WebAPI.Dtos.AboutDtos;
using ApiProjeKampi.WebAPI.Dtos.CategoryDtos;
using ApiProjeKampi.WebAPI.Dtos.FeatureDtos;
using ApiProjeKampi.WebAPI.Dtos.ImageDtos;
using ApiProjeKampi.WebAPI.Dtos.MessageDtos;
using ApiProjeKampi.WebAPI.Dtos.NotificationDtos;
using ApiProjeKampi.WebAPI.Dtos.ProductDtos;
using ApiProjeKampi.WebAPI.Dtos.ReservationDtos;
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

        CreateMap<Notification, ResultNotificationDto>().ReverseMap();
        CreateMap<Notification, CreateNotificationDto>().ReverseMap();
        CreateMap<Notification, UpdateNotificationDto>().ReverseMap();
        CreateMap<Notification, GetByIdNotificationDto>().ReverseMap();

        CreateMap<Category, ResultCategoryDto>().ReverseMap();
        CreateMap<Category, CreateCategoryDto>().ReverseMap();
        CreateMap<Category, UpdateCategoryDto>().ReverseMap();
        CreateMap<Category, GetCategoryByIdDto>().ReverseMap();

        CreateMap<About, ResultAboutDto>().ReverseMap();
        CreateMap<About, CreateAboutDto>().ReverseMap();
        CreateMap<About, UpdateAboutDto>().ReverseMap();
        CreateMap<About, GetAboutByIdDto>().ReverseMap();

        CreateMap<Reservation, ResultReservationDto>().ReverseMap();
        CreateMap<Reservation, CreateReservationDto>().ReverseMap();
        CreateMap<Reservation, UpdateReservationDto>().ReverseMap();
        CreateMap<Reservation, GetReservationByIdDto>().ReverseMap();

        CreateMap<Image, ResultImageDto>().ReverseMap();
        CreateMap<Image, CreateImageDto>().ReverseMap();
        CreateMap<Image, UpdateImageDto>().ReverseMap();
        CreateMap<Image, GetImageByIdDto>().ReverseMap();
    }
}