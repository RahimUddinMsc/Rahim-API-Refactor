using MediatR;
using RefactoringChallenge.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RefactorChallenge.Application.Orders.Queries.Queries.GetOrderList
{
    public class GetOrderListQuery : IRequest<List<OrderResponse>>
    {
    }
}
