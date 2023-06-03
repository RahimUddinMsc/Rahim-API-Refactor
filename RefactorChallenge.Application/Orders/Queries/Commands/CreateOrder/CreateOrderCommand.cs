using MediatR;
using RefactorChallenge.Application.ViewModels;
using RefactoringChallenge.Application.ViewModels;
using RefactoringChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RefactorChallenge.Application.Orders.Queries.Commands
{
    public class CreateOrderCommand : IRequest<OrderResponse>
    {
        public string CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? RequiredDate { get; set; }
        public int? ShipVia { get; set; }
        public decimal? Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
        public IEnumerable<OrderDetailCreateRequest> OrderDetails { get; set; }
    }
}
