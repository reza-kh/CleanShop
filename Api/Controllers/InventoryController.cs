using Application.Inventory.Commands.AddInventoryItem;
using Application.Inventory.Queries.GetAllInventory;
using Application.Inventory.Queries.GetInventoryByProduct;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InventoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/inventory
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllInventoryQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/inventory/{productId}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid productId)
        {
            var query = new GetInventoryByProductQuery() { ProductId=productId};
            var result = await _mediator.Send(query);
            if (result is null) return NotFound();
            return Ok(result);
        }


        // POST: api/inventory/create
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] AddInventoryItemCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

    }
}
