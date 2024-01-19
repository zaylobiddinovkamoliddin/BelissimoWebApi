using Belissimo.Dtos.OrderDtos;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Imtihon_Belissimo_4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _orderService.GetOrderById(id);
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddOrderDto addOrderDto)
        {
            if (ModelState.IsValid)
            {
                await _orderService.AddOrderAsync(addOrderDto);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateOrderDto updateOrderDto)
        {
            if(ModelState.IsValid)
            {
                await _orderService.UpdateOrderAsync(updateOrderDto);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return Ok();
        }

    }
}
