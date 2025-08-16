using Application.Orders.Commands.AdvanceOrderStatus;
using Application.Orders.Commands.CreateOrder;
using Application.Orders.DTOs;
using Application.Orders.Queries.GetOrderById;
using Application.Orders.Queries.GetOrders;
using Domain.Orders.Enum;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid? customerId,
                                                [FromQuery] OrderStatus? status,
                                                [FromQuery] DateTime? createdFrom,
                                                [FromQuery] DateTime? createdTo,
                                                [FromQuery] int pageIndex = 1,
                                                [FromQuery] int pageSize = 10)
        {
            var filter = new OrderFilterDto
            {
                CustomerId = customerId,
                Status = status,
                CreatedFrom = createdFrom,
                CreatedTo = createdTo
            };
            var query = new GetOrdersQuery
            {
                Filter = filter,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/orders/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetOrderByIdQuery(id);
            var order = await _mediator.Send(query);
            if (order is null) return NotFound();
            return Ok(order);
        }

        // POST: api/orders/create
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        {
            var orderId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = orderId }, orderId);
        }

        // PUT: api/orders/update-status
        [HttpPost("update-status")]
        public async Task<IActionResult> UpdateStatus([FromBody] AdvanceOrderStatusCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
