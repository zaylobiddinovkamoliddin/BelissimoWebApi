using Belissimo.Dtos.OrderItemDtos;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Imtihon_Belissimo_4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrdetItemService _ordetItemService;

        public OrderItemController(IOrdetItemService ordetItemService)
        {
            _ordetItemService = ordetItemService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orderitem = await _ordetItemService.GetAllOrderItemsAsync();
            return Ok(orderitem);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ordetitem = await _ordetItemService.GetOrdetItemDtoByIdAsync(id);
            return Ok(ordetitem);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddOrderItemDto addOrderItemDto)
        {
            if (ModelState.IsValid)
            {
                await _ordetItemService.AddOrdetItemAsync(addOrderItemDto);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateOrdetItemDto updateOrdetItemDto)
        {
            if (ModelState.IsValid)
            {
                await _ordetItemService.UpdateOrdetItemAsync(updateOrdetItemDto);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _ordetItemService.DeleteOrdetItemAsync(id);
            return Ok();
        }
    }
}
