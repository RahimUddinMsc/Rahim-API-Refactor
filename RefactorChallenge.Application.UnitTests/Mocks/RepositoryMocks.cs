using Moq;
using RefactorChallenge.Application.Contracts;
using RefactoringChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RefactorChallenge.Application.UnitTests.Mocks
{
    public class RepositoryMocks
    {
        public static List<OrderDetail> orderDetails = new List<OrderDetail>
        {
            new OrderDetail
            {
                OrderId = 1,
                ProductId = 1001,
                UnitPrice = 101,
                Quantity = 10,
                Discount = 0.25f,
            },
            new OrderDetail
            {
                OrderId = 1,
                ProductId = 1002,
                UnitPrice = 102,
                Quantity = 20,
                Discount = 0.25f,
            },
            new OrderDetail
            {
                OrderId = 2,
                ProductId = 2001,
                UnitPrice = 201,
                Quantity = 20,
                Discount = 0.5f,
            },
            new OrderDetail
            {
                OrderId = 3,
                ProductId = 3001,
                UnitPrice = 303,
                Quantity = 30,
                Discount = 0.75f,
            }
        };

        public static Mock<IAsyncRepository<Order>> GetOrderRepository()
        {
            var orders = new List<Order>
            {
                new Order
                {

                    OrderId = 1,
                    CustomerId = "Alpha",
                    EmployeeId = 1,
                    OrderDate = DateTime.Now,
                    RequiredDate = DateTime.Now,
                    ShippedDate = DateTime.Now,
                    ShipVia = 1,
                    Freight = 1,
                    ShipName = "Titanic",
                    ShipAddress = "59 rue de l'Abbaye",
                    ShipCity = "Reims",
                    ShipRegion = null,
                    ShipPostalCode = "51100",
                    ShipCountry = "France",
                    OrderDetails = orderDetails.Where(s => s.OrderId == 1).ToList(),
                },
                new Order
                {

                    OrderId = 2,
                    CustomerId = "Beta",
                    EmployeeId = 1,
                    OrderDate = DateTime.Now,
                    RequiredDate = DateTime.Now,
                    ShippedDate = DateTime.Now,
                    ShipVia = 1,
                    Freight = 1,
                    ShipName = "Titanic",
                    ShipAddress = "59 rue de l'Abbaye",
                    ShipCity = "Reims",
                    ShipRegion = null,
                    ShipPostalCode = "51100",
                    ShipCountry = "France",
                    OrderDetails = orderDetails.Where(s => s.OrderId == 2).ToList(),
                },
                new Order
                {

                    OrderId = 3,
                    CustomerId = "Charlie",
                    EmployeeId = 1,
                    OrderDate = DateTime.Now,
                    RequiredDate = DateTime.Now,
                    ShippedDate = DateTime.Now,
                    ShipVia = 1,
                    Freight = 1,
                    ShipName = "Titanic",
                    ShipAddress = "59 rue de l'Abbaye",
                    ShipCity = "Reims",
                    ShipRegion = null,
                    ShipPostalCode = "51100",
                    ShipCountry = "France",
                    OrderDetails = orderDetails.Where(s => s.OrderId == 3).ToList(),
                }
            };


            var mockOrderRepository =  new Mock<IAsyncRepository<Order>>();
            mockOrderRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(orders);
            
            mockOrderRepository.Setup(repo => repo.QueryableList()).Returns(orders.AsQueryable());

            mockOrderRepository.Setup(repo => repo.QueryableList()).Returns(orders.AsQueryable());

            mockOrderRepository.Setup(repo => repo.AddAsync(It.IsAny<Order>())).ReturnsAsync(
                (Order order) =>
                {
                    orders.Add(order);
                    return order;
                });

            return mockOrderRepository;
        }

        public static Mock<IAsyncRepository<OrderDetail>> GetOrderDetailRepository()
        {
            var orderDetailList = orderDetails;

            var mockOrderDetailRepository = new Mock<IAsyncRepository<OrderDetail>>();
            mockOrderDetailRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(orderDetailList);                      

            mockOrderDetailRepository.Setup(repo => repo.AddAsync(It.IsAny<OrderDetail>())).ReturnsAsync(
                (OrderDetail orderDetail) =>
                {
                    orderDetailList.Add(orderDetail);
                    return orderDetail;
                });

            return mockOrderDetailRepository;
        }
    }
}
