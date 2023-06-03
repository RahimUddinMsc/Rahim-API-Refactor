using MediatR;
using RefactorChallenge.Application.Contracts;
using RefactorChallenge.Application.Exceptions;
using RefactoringChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RefactorChallenge.Application.Orders.Queries.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IAsyncRepository<Order> _orders;
        private readonly IAsyncRepository<OrderDetail> _orderDetails;
       
        public DeleteOrderCommandHandler(IAsyncRepository<Order> orders, IAsyncRepository<OrderDetail> orderDetails)
        {            
            _orders = orders;
            _orderDetails = orderDetails;
        }

        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {            
            var order = await _orders.GetByIdAsync(request.OrderId);
            if(order == null)
                throw new NotFoundException(nameof(Order), request.OrderId);

            var orderDetails = await _orderDetails.Find(o => o.OrderId == request.OrderId);
            if (orderDetails.Count > 0)
                await _orderDetails.DeleteAllAsync(orderDetails);

            await _orders.DeleteAsync(order);           
        }
    }
}
