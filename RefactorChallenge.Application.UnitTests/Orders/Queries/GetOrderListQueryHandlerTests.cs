using AutoMapper;
using Moq;
using RefactorChallenge.Application.Contracts;
using RefactorChallenge.Application.Orders.Queries.Queries.GetOrder;
using RefactorChallenge.Application.Orders.Queries.Queries.GetOrderList;
using RefactorChallenge.Application.Profiles;
using RefactorChallenge.Application.UnitTests.Mocks;
using RefactoringChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RefactorChallenge.Application.UnitTests.Orders.Queries
{
    public class GetOrderListQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<Order>> _mockOrderRepository;
        private readonly Mock<IAsyncRepository<OrderDetail>> _mockOrderDetailRepository;

        public GetOrderListQueryHandlerTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
            _mockOrderRepository = RepositoryMocks.GetOrderRepository();
            _mockOrderDetailRepository = RepositoryMocks.GetOrderDetailRepository();            
        }

        [Fact]
        public async Task GetOrderListQuery_ShouldReturnItems()
        {            
            var handler = new GetOrderListQueryHandler(_mapper, _mockOrderRepository.Object, _mockOrderDetailRepository.Object);
            var result = await handler.Handle(new GetOrderListQuery(), CancellationToken.None);            
         
            Assert.True(result.Count > 0);
        }

        [Fact]
        public async Task GetOrderListQuery_ShouldSkipOneTakeOne()
        {
            var expectedOrderId = 2;
            
            var handler = new GetOrderListQueryHandler(_mapper, _mockOrderRepository.Object, _mockOrderDetailRepository.Object);
            var result = await handler.Handle(new GetOrderListQuery() {Skip = 1, Take = 1}, CancellationToken.None);
            
            Assert.True(result[0].OrderId == expectedOrderId);
        }
    }
}
