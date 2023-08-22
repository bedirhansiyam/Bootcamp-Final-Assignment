using AutoMapper;
using WebApi.Data;

namespace WebApi.Schema;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories.Select(x => x.Category.Name)));
        CreateMap<ProductRequest, Product>();

        CreateMap<Category, CategoryResponse>();
        CreateMap<CategoryRequest, Category>();

        CreateMap<User, UserResponse>();
        CreateMap<UserRequest, User>();
        CreateMap<User, LoyaltyPointsResponse>()
            .ForMember(dest => dest.MyLoyaltyPoints, opt => opt.MapFrom(src => src.LoyaltyPoints));

        CreateMap<Coupon, CouponResponse>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
        CreateMap<CouponRequest, Coupon>();

        CreateMap<Basket, BasketResponse>()
            .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price));
        CreateMap<BasketRequest, Basket>();

        CreateMap<Order, OrderResponse>();

        CreateMap<OrderDetail, OrderDetailResponse>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
    }
}
