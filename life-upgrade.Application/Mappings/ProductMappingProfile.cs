using AutoMapper;
using LifeUpgrade.Application.ApplicationUser;
using LifeUpgrade.Application.Photo.Commands;
using LifeUpgrade.Application.Product;
using LifeUpgrade.Application.Product.Commands.EditProduct;
using LifeUpgrade.Application.WebShop;
using LifeUpgrade.Domain.Entities;

namespace LifeUpgrade.Application.Mappings;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile(IUserContext userContext)
    {
        var user = userContext.GetCurrentUser();
        
        CreateMap<ProductDto, Domain.Entities.Product>()
            .ForMember(e => e.Details, opt => opt.MapFrom(src => new ProductDetails()
            {
                Type = src.Type,
            }));

        CreateMap<Domain.Entities.Product, ProductDto>()
            .ForMember(dto => dto.Type, opt => opt.MapFrom(src => src.Details.Type))
            .ForMember(dto => dto.IsEditable, opt => opt.MapFrom(src => src.Details.CreatedById == user.Id));
        
        CreateMap<WebShopDto, Domain.Entities.WebShop>()
            .ReverseMap();
        
        CreateMap<Domain.Entities.Photo, PhotoDto>()
            .ReverseMap();

        CreateMap<ProductDto, EditProductCommand>();
    }
}