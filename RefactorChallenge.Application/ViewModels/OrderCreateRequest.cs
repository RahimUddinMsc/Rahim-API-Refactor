using System;
using System.Collections.Generic;
using System.Text;

namespace RefactorChallenge.Application.ViewModels
{
    public class OrderCreateRequest
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
        string ShipPostalCode { get; set; }
        string ShipCountry { get; set; }

        IEnumerable<OrderDetailCreateRequest> OrderDetails { get; set; }

    }
}
