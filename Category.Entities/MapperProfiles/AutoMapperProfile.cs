using AutoMapper;
using Category.Entities.DTOs;

namespace Category.Entities.MapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CategoryModel, Entities.Category>().ReverseMap();
            CreateMap<CreateCategoryModel, Entities.Category>().ReverseMap();
        }
    }
}
