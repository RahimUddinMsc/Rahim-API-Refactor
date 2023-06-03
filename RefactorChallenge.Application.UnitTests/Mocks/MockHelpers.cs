using RefactorChallenge.Application.Orders.Queries.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace RefactorChallenge.Application.UnitTests.Mocks
{
    public static class MockHelpers
    {
        public static CreateOrderCommand GetCreateOrderCommand(bool valid)
        {
            var orderCommand = new CreateOrderCommand
            {
                CustomerId = valid ? "Delta" : null,
                EmployeeId = 4,
                RequiredDate = DateTime.Now,
                ShipVia = 4,
                Freight = 4,
                ShipName = "Titanic v4",
                ShipAddress = "59 rue de l'Abbaye",
                ShipCity = "Reims",
                ShipRegion = null,
                ShipPostalCode = "51100",
                ShipCountry = "France",
            };

            return orderCommand;
        }
    }
}
