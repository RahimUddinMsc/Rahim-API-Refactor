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

namespace RefactorChallenge.Application.Orders.Queries.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderResponse>
    {
        private readonly IAsyncRepository<Order> _orders;        
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IMapper mapper, IAsyncRepository<Order> orders)
        {
            _mapper = mapper;
            _orders = orders;            
        }

        public async Task<OrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);

            await _orders.AddAsync(order);
            
            return _mapper.Map<OrderResponse>(order);
        }
    }
}
