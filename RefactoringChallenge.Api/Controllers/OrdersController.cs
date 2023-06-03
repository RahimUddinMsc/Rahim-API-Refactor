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
        
        [HttpGet("{orderId}", Name = "GetOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderResponse>> GetById([FromRoute] int orderId)
        {            
            var data = await _mediator.Send(new GetOrderQuery() { OrderId = orderId });
            return Ok(data);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderResponse>> Create([FromBody] CreateOrderCommand order)
        {
            var response = await _mediator.Send(order);
            return CreatedAtRoute("GetOrder", new { orderId = response.OrderId }, response);                                
        }

        //Order route attribute no longer needed kept to maintain original api call
        [HttpPost("{orderId}/[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderResponse>> AddProductsToOrder([FromBody] AddProductToOrderCommand orderDetails)
        {
            var response = await _mediator.Send(orderDetails);
            return CreatedAtRoute("GetOrder", new { orderId = orderDetails.OrderId }, response);            
        }

        [HttpPost("{orderId}/[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] int orderId)
        {            
            await _mediator.Send(new DeleteOrderCommand() { OrderId = orderId });   
            return NoContent();
        }
    }
}
