using AutoMapper;
using MediatR;
using RefactorChallenge.Application.Contracts;
using RefactoringChallenge.Application.ViewModels;
using RefactoringChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RefactorChallenge.Application.Exceptions;

namespace RefactorChallenge.Application.Orders.Queries.Queries.GetOrder
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderResponse>
    {

        private readonly IAsyncRepository<Order> _orders;
        private readonly IAsyncRepository<OrderDetail> _orderDetails;
        private readonly IMapper _mapper;

        public GetOrderQueryHandler(IMapper mapper, IAsyncRepository<Order> orders, IAsyncRepository<OrderDetail> orderDetails)
        {
            _mapper = mapper;
            _orders = orders;
            _orderDetails = orderDetails;
        }

        public async Task<OrderResponse> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _orders.GetByIdAsync(request.OrderId);
            if (order == null)
                throw new NotFoundException(nameof(Order),request.OrderId);

            var orderDetails = await _orderDetails.Find(o => o.OrderId == order.OrderId);            
            order.OrderDetails = orderDetails;
            
            return _mapper.Map<OrderResponse>(order);            
        }
    }
}
