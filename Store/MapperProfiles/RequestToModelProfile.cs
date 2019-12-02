using AutoMapper;
using Store.Contracts.V1.Requests;
using Store.Models;

namespace Store.MapperProfiles
{
    public class RequestToModelProfile : Profile
    {
        public RequestToModelProfile()
        {
            CreateMap<ProductRequest, Product>();

            CreateMap<CategoryRequest, Category>();

            CreateMap<PaginationRequest, PaginationFilter>();
        }
    }
}
