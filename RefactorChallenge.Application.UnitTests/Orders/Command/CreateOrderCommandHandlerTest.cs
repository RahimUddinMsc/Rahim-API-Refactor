using AutoMapper;
using Moq;
using RefactorChallenge.Application.Contracts;
using RefactorChallenge.Application.Exceptions;
using RefactorChallenge.Application.Orders.Queries.Commands;
using RefactorChallenge.Application.Profiles;
using RefactorChallenge.Application.UnitTests.Mocks;
using RefactoringChallenge.Application.ViewModels;
using RefactoringChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RefactorChallenge.Application.UnitTests.Orders.Command
{
    public class CreateOrderCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<Order>> _mockOrderRepository;
        private readonly Mock<IAsyncRepository<OrderDetail>> _mockOrderDetailRepository;

        public CreateOrderCommandHandlerTest()
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
        public async Task CreateOrderCommand_ShouldAddOrderRecord()
        {
            var handler = new CreateOrderCommandHandler(_mapper, _mockOrderRepository.Object);
            var result = await handler.Handle(MockHelpers.GetCreateOrderCommand(true), CancellationToken.None);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateOrderCommand_ShouldThrowNotFoundError()
        {
            var handler = new CreateOrderCommandHandler(_mapper, _mockOrderRepository.Object);

            await Assert.ThrowsAsync<BadRequestException>(
                async () => await handler.Handle(MockHelpers.GetCreateOrderCommand(false), CancellationToken.None));            
        }       
    }
}
