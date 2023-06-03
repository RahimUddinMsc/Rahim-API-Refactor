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

namespace RefactorChallenge.Application.Orders.Queries.Commands.AddProductToOrder
{
    public class AddProductToOrderCommandHandler : IRequestHandler<AddProductToOrderCommand, IEnumerable<OrderDetailResponse>>
    {
        private readonly IAsyncRepository<OrderDetail> _orderDetails;        
        private readonly IMapper _mapper;

        public AddProductToOrderCommandHandler(IMapper mapper, IAsyncRepository<OrderDetail> orderDetails)
        {
            _mapper = mapper;
            _orderDetails = orderDetails;            
        }

        public async Task<IEnumerable<OrderDetailResponse>> Handle(AddProductToOrderCommand request, CancellationToken cancellationToken)
        {
            //Todo 
            //1. Add products exist check and throw exception
            //2. optional if have time add additional check to see if product allready in db in orders and add to quantity else will throw error

            var orderDetails = _mapper.Map<List<OrderDetail>>(request.OrderDetails);
            orderDetails.ForEach(orderDetail => orderDetail.OrderId = request.OrderId);

            await _orderDetails.AddRange(_mapper.Map<List<OrderDetail>>(orderDetails));            
                        
            return _mapper.Map<IEnumerable<OrderDetailResponse>>(request.OrderDetails);
        }
    }
}
