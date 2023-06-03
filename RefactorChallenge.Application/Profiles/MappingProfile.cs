using AutoMapper;
using RefactorChallenge.Application.Orders.Queries.Commands;
using RefactorChallenge.Application.ViewModels;
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
            CreateMap<OrderDetailResponse, OrderDetailCreateRequest>().ReverseMap();

            CreateMap<CreateOrderCommand, Order>().ReverseMap();
            CreateMap<OrderDetailCreateRequest, OrderDetail>().ReverseMap();            
        }
    }
}
