using System;
using System.Collections.Generic;
using System.Text;

namespace RefactorChallenge.Application.ViewModels
{
    public class OrderDetailCreateRequest
    {
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }

    }
}
