using AutoMapper;
using MediatR;
using RefactorChallenge.Application.Contracts;
using RefactorChallenge.Application.Exceptions;
using RefactoringChallenge.Application.ViewModels;
using RefactoringChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RefactorChallenge.Application.Orders.Queries.Commands.AddProductToOrder
{
    public class AddProductToOrderCommandHandler : IRequestHandler<AddProductToOrderCommand, IEnumerable<OrderDetailResponse>>
    {
        private readonly IAsyncRepository<OrderDetail> _orderDetails;
        private readonly IAsyncRepository<Order> _orders;
        private readonly IMapper _mapper;

        public AddProductToOrderCommandHandler(IMapper mapper, IAsyncRepository<OrderDetail> orderDetails, IAsyncRepository<Order> orders)
        {
            _mapper = mapper;
            _orderDetails = orderDetails;            
            _orders = orders;
        }

        public async Task<IEnumerable<OrderDetailResponse>> Handle(AddProductToOrderCommand request, CancellationToken cancellationToken)
        {
            //Todo 
            //1. Add order exist check and throw exception
            //2. optional if have time add additional check to see if product allready in db in orders and add to quantity else will throw error

            var order = await _orders.GetByIdAsync(request.OrderId);
            if(order == null)
                throw new NotFoundException(nameof(Order), request.OrderId);
            
            var orderDetails = _mapper.Map<List<OrderDetail>>(request.OrderDetails);
            orderDetails.ForEach(orderDetail => orderDetail.OrderId = request.OrderId);
            await _orderDetails.AddRange(_mapper.Map<List<OrderDetail>>(orderDetails));            
                        
            return _mapper.Map<IEnumerable<OrderDetailResponse>>(request.OrderDetails);
        }
    }
}
