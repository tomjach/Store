using AutoMapper;
using Store.Contracts.V1.Responses;
using Store.Models;

namespace Store.MapperProfiles
{
    public class ModelToResponseProfile : Profile
    {
        public ModelToResponseProfile()
        {
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.CategoryName, x => x.MapFrom(src => src.Category.Name));

            CreateMap<Category, CategoryResponse>();
        }
    }
}
