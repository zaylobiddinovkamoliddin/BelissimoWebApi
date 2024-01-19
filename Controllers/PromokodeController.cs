using Belissimo.Dtos.PromokodeDtos;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Imtihon_Belissimo_4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromokodeController : ControllerBase
    {
        private readonly IPromokodeService _promokodeService;

        public PromokodeController(IPromokodeService promokodeService)
        {
            _promokodeService = promokodeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
           var promo =  await _promokodeService.GetAllPromokodesAsync();
            return Ok(promo);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var promo = await _promokodeService.GetPromokodeByIdAsync(id);
            return Ok(promo);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddPromokodeDto addPromokodeDto)
        {
            if (ModelState.IsValid)
            {
                await _promokodeService.AddPromokodeAsync(addPromokodeDto);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdatePromokodeDto updatePromokodeDto)
        {
            if (ModelState.IsValid)
            {
                await _promokodeService.UpdatePromokodeAsync(updatePromokodeDto);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _promokodeService.DeletePromokodeAsync(id);
            return Ok();    
        }


    }
}
