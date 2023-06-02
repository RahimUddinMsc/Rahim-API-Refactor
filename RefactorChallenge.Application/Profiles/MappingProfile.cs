using AutoMapper;
using RefactoringChallenge.Application.ViewModels;
using RefactoringChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RefactorChallenge.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order,OrderResponse>().ReverseMap();
            CreateMap<OrderDetail,OrderDetailResponse>().ReverseMap();
        }
    }
}
