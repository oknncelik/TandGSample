using AutoMapper;
using Product.Entities.DTOs;

namespace Product.Entities.MapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductModel, Entities.Product>().ReverseMap();
            CreateMap<CreateProductModel, Entities.Product >().ReverseMap();
        }
    }
}
