using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RefactorChallenge.Application.Orders.Queries.Commands;
using RefactorChallenge.Application.Orders.Queries.Commands.AddProductToOrder;
using RefactorChallenge.Application.Orders.Queries.Commands.DeleteOrder;
using RefactorChallenge.Application.Orders.Queries.Queries.GetOrder;
using RefactorChallenge.Application.Orders.Queries.Queries.GetOrderList;
using RefactorChallenge.Application.ViewModels;
using RefactoringChallenge.Application.ViewModels;
using RefactoringChallenge.Entities;

namespace RefactoringChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        //private readonly NorthwindDbContext _northwindDbContext;
        //private readonly IMapper _mapper;

        //public OrdersController(NorthwindDbContext northwindDbContext, IMapper mapper)
        //{
        //    _northwindDbContext = northwindDbContext;
        //    _mapper = mapper;
        //}

        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<OrderResponse>>> Get(bool? includeOrderDetails, int? skip = null, int? take = null)
        {
            var getOrderListQuery = new GetOrderListQuery() { IncludeOrderDetails = includeOrderDetails, Skip = skip, Take = take};
            var data = await _mediator.Send(getOrderListQuery);

            return Ok(data);
        }

        //[HttpGet]
        //public IActionResult Get(int? skip = null, int? take = null)
        //{
        //    var query = _northwindDbContext.Orders;
        //    if (skip != null)
        //    {
        //        query.Skip(skip.Value);
        //    }
        //    if (take != null)
        //    {
        //        query.Take(take.Value);
        //    }
        //    var result = _mapper.From(query).ProjectToType<OrderResponse>().ToList();
        //    return Json(result);
        //}


        [HttpGet("{orderId}", Name = "GetOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OrderResponse>> GetById([FromRoute] int orderId)
        {            
            var data = await _mediator.Send(new GetOrderQuery() { OrderId = orderId });

            //Use middleware
            //if (result == null)
            //    return NotFound();

            return Ok(data);
        }


        //[HttpGet("{orderId}")]
        //public IActionResult GetById([FromRoute] int orderId)
        //{
        //    var result = _mapper.From(_northwindDbContext.Orders).ProjectToType<OrderResponse>().FirstOrDefault(o => o.OrderId == orderId);

        //    if (result == null)
        //        return NotFound();

        //    return Json(result);
        //}


        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<OrderResponse>> Create([FromBody] CreateOrderCommand order)
        {
            var response = await _mediator.Send(order);
            return CreatedAtRoute("GetOrder", new { orderId = response.OrderId }, response);                                
        }


        //[HttpPost("[action]")]
        //public IActionResult Create(
        //    string customerId,
        //    int? employeeId,
        //    DateTime? requiredDate,
        //    int? shipVia,
        //    decimal? freight,
        //    string shipName,
        //    string shipAddress,
        //    string shipCity,
        //    string shipRegion,
        //    string shipPostalCode,
        //    string shipCountry,
        //    IEnumerable<OrderDetailRequest> orderDetails
        //    )
        //{
        //    var newOrderDetails = new List<OrderDetail>();
        //    foreach (var orderDetail in orderDetails)
        //    {
        //        newOrderDetails.Add(new OrderDetail
        //        {
        //            ProductId = orderDetail.ProductId,
        //            Discount = orderDetail.Discount,
        //            Quantity = orderDetail.Quantity,
        //            UnitPrice = orderDetail.UnitPrice,
        //        });
        //    }

        //    var newOrder = new Order
        //    {
        //        CustomerId = customerId,
        //        EmployeeId = employeeId,
        //        OrderDate = DateTime.Now,
        //        RequiredDate = requiredDate,
        //        ShipVia = shipVia,
        //        Freight = freight,
        //        ShipName = shipName,
        //        ShipAddress = shipAddress,
        //        ShipCity = shipCity,
        //        ShipRegion = shipRegion,
        //        ShipPostalCode = shipPostalCode,
        //        ShipCountry = shipCountry,
        //        OrderDetails = newOrderDetails,
        //    };
        //    _northwindDbContext.Orders.Add(newOrder);
        //    _northwindDbContext.SaveChanges();

        //    return Json(newOrder.Adapt<OrderResponse>());
        //}

        //Order route attribute no longer needed kept to maintain original api call
        [HttpPost("{orderId}/[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<OrderResponse>> AddProductsToOrder([FromBody] AddProductToOrderCommand orderDetails)
        {
            var response = await _mediator.Send(orderDetails);
            return CreatedAtRoute("GetOrder", new { orderId = orderDetails.OrderId }, response);            
        }


        //[HttpPost("{orderId}/[action]")]
        //public IActionResult AddProductsToOrder([FromRoute] int orderId, IEnumerable<OrderDetailRequest> orderDetails)
        //{
        //    var order = _northwindDbContext.Orders.FirstOrDefault(o => o.OrderId == orderId);
        //    if (order == null)
        //        return NotFound();

        //    var newOrderDetails = new List<OrderDetail>();
        //    foreach (var orderDetail in orderDetails)
        //    {
        //        newOrderDetails.Add(new OrderDetail
        //        {
        //            OrderId = orderId,
        //            ProductId = orderDetail.ProductId,
        //            Discount = orderDetail.Discount,
        //            Quantity = orderDetail.Quantity,
        //            UnitPrice = orderDetail.UnitPrice,
        //        });
        //    }

        //    _northwindDbContext.OrderDetails.AddRange(newOrderDetails);
        //    _northwindDbContext.SaveChanges();

        //    return Json(newOrderDetails.Select(od => od.Adapt<OrderDetailResponse>()));
        //}


        [HttpPost("{orderId}/[action]")]
        public async Task<ActionResult> Delete([FromRoute] int orderId)
        {
            var deleletOrderCommand = new DeleteOrderCommand() { OrderId = orderId };
            await _mediator.Send(deleletOrderCommand);   

            return NoContent();
        }


        //[HttpPost("{orderId}/[action]")]
        //public IActionResult Delete([FromRoute] int orderId)
        //{
        //    var order = _northwindDbContext.Orders.FirstOrDefault(o => o.OrderId == orderId);
        //    if (order == null)
        //        return NotFound();

        //    var orderDetails = _northwindDbContext.OrderDetails.Where(od => od.OrderId == orderId);

        //    _northwindDbContext.OrderDetails.RemoveRange(orderDetails);
        //    _northwindDbContext.Orders.Remove(order);
        //    _northwindDbContext.SaveChanges();

        //    return Ok();
        //}
    }
}
