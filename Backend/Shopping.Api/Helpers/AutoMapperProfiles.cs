using AutoMapper;
using Shopping.Api.DTO.ArticleDTO;
using Shopping.Api.DTO.ItemDTO;
using Shopping.Api.DTO.OrderDTO;
using Shopping.Api.DTO.UserDTO;
using Shopping.Api.Models;

namespace Shopping.Api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //USER
            //register user
            CreateMap<RegisterUserDto, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordKey, opt => opt.Ignore());

            //update user
            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordKey, opt => opt.Ignore());

            //ARTICLE
            //create article
            CreateMap<CreateArticleDto, Article>();

            //ORDER
            //create order
            CreateMap<CreateOrderDto,  Order>()
                .ForMember(dest => dest.Item, opt => opt.MapFrom(src => src.Item));
            CreateMap<CreateOrderItemDto, Item>();
            CreateMap<Order, GetCreatedOrderDto>()
                .ForMember(dest => dest.Item, opt => opt.MapFrom(src => src.Item));
            CreateMap<Item, GetCreatedOrderItemDto>()
                .ForMember(dest => dest.ArticleName, opt => opt.MapFrom(src => src.Article.Name))
                .ForMember(dest => dest.ArticlePrice, opt => opt.MapFrom(src => src.Article.Price));

            //get order history for customer and seller
            CreateMap<Order, HistoryOrderDto>()
                .ForMember(dest => dest.Item, opt => opt.MapFrom(src => src.Item));
            CreateMap<Item, HistoryOrderItemDto>();

            //get all orders
            CreateMap<Order, GetAllOrderDto>()
                .ForMember(dest => dest.Item, opt => opt.MapFrom(src => src.Item));
            CreateMap<Item, GetAllOrderItemDto>()
                .ForMember(dest => dest.ArticleName, opt => opt.MapFrom(src => src.Article.Name))
                .ForMember(dest => dest.ArticlePrice, opt => opt.MapFrom(src => src.Article.Price));
        }
    }
}
