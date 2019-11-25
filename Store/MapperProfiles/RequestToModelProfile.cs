﻿using AutoMapper;
using Store.Contracts.V1.Requests;
using Store.Models;

namespace Store.MapperProfiles
{
    public class RequestToModelProfile : Profile
    {
        public RequestToModelProfile()
        {
            CreateMap<ProductRequest, Product>()
                .ForMember(dest => dest.Name, x => x.MapFrom(src => src.Name2));
        }
    }
}