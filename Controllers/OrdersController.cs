using Lab00_Sova.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab00_Sova.Controllers
{
    [Route("api/orders")]
    public sealed class OrdersController(IOrderService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetAll()
        {
            var orders = await service.GetAllAsync();
            return Ok(orders.Select(order => order.ToDto()).ToList());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderDto>> GetById(Guid id)
        {
            var order = await service.GetByIdAsync(id);
            return order is null ? NotFound() : Ok(order.ToDto());
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create(CreateOrderRequest request)
        {
            try
            {
                var order = await service.CreateAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = order.Id }, order.ToDto());
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(new { error = exception.Message });
            }
        }
    }



}
