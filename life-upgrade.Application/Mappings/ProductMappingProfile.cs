using AutoMapper;
using LifeUpgrade.Application.Product;
using LifeUpgrade.Domain.Entities;

namespace LifeUpgrade.Application.Mappings;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<ProductDto, Domain.Entities.Product>()
            .ForMember(e => e.Details, opt => opt.MapFrom(src => new ProductDetails()
            {
                Type = src.Type,
            }));
    }
}