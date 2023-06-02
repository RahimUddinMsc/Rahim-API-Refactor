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

namespace RefactorChallenge.Application.Orders.Queries.Queries.GetOrderList
{
    public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, List<OrderResponse>>
    {

        private readonly IAsyncRepository<Order> _repo;
        private readonly IMapper _mapper;

        public GetOrderListQueryHandler(IMapper mapper, IAsyncRepository<Order> repo) 
        {
            _mapper = mapper;
            _repo = repo;   
        }

        public async Task<List<OrderResponse>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
            var allOrders = await _repo.ListAllAsync();                        
            return _mapper.Map<List<OrderResponse>>(allOrders);
        }
    }
}
