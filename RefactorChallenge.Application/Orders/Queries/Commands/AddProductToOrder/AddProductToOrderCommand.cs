using MediatR;
using RefactorChallenge.Application.ViewModels;
using RefactoringChallenge.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RefactorChallenge.Application.Orders.Queries.Commands.AddProductToOrder
{
    public class AddProductToOrderCommand : IRequest<IEnumerable<OrderDetailResponse>>
    {
        public int OrderId { get; set; }

        public ICollection<OrderDetailCreateRequest> OrderDetails { get; set; }

    }
}
