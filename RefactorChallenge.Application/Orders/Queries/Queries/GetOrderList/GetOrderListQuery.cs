using MediatR;
using RefactoringChallenge.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RefactorChallenge.Application.Orders.Queries.Queries.GetOrderList
{
    public class GetOrderListQuery : IRequest<List<OrderResponse>>
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public bool? IncludeOrderDetails { get; set; } 
    }    
}
