using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RefactorChallenge.Application.Orders.Queries.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest
    {
        public int OrderId { get; set; }    
    }
}
