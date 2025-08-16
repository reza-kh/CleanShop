using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Queries.GetAllCustomers;
using Application.Customers.Queries.GetCustomerById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize]
        // GET: api/customers
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllCustomersQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/customers/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetCustomerByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/customers/create
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }


    }
}
