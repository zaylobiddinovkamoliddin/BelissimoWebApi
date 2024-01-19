using AutoMapper.Configuration;
using Belissimo.Dtos.UserDto;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer.Repasitories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Imtihon_Belissimo_4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var user  = await _userService.GetUseByIdAsync(Id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddUserDto addUserDto)
        {
           if (ModelState.IsValid)
            {
                await _userService.AddUserAsync(addUserDto);
                return Ok();
            }
           return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateUserDto updateUserDto)
        {
            if (ModelState.IsValid)
            {
                await _userService.UpdatUserAsync(updateUserDto);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok();
        }


    }
}
