using AutoMapper;
using MediatR;
using RefactorChallenge.Application.Contracts;
using RefactoringChallenge.Application.ViewModels;
using RefactoringChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace RefactorChallenge.Application.Orders.Queries.Queries.GetOrderList
{
    public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, List<OrderResponse>>
    {

        private readonly IAsyncRepository<Order> _orders;
        private readonly IAsyncRepository<OrderDetail> _orderDetails;
        private readonly IMapper _mapper;

        public GetOrderListQueryHandler(IMapper mapper, IAsyncRepository<Order> orders, IAsyncRepository<OrderDetail> orderDetails) 
        {
            _mapper = mapper;
            _orders = orders;   
            _orderDetails = orderDetails;   
        }

        public async Task<List<OrderResponse>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
            var orders = _orders.QueryableList();

            if (request.Skip.HasValue)
                orders = orders.Skip(request.Skip.Value);

            if (request.Take.HasValue)
                orders = orders.Take(request.Take.Value);
            
            if(request.IncludeOrderDetails.HasValue && request.IncludeOrderDetails.Value)
            {
                foreach(var order in orders)
                {
                    order.OrderDetails = await _orderDetails.Find(o => o.OrderId == order.OrderId);
                }
            }
                
            return _mapper.Map<List<OrderResponse>>(orders);
        }
    }
}
