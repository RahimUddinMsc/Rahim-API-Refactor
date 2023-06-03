using MediatR;
using RefactoringChallenge.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RefactorChallenge.Application.Orders.Queries.Queries.GetOrder
{
    public class GetOrderQuery : IRequest<OrderResponse>
    {
        public int OrderId { get; set; } 
    }
}
