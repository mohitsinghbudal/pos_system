using Microsoft.AspNetCore.Mvc;
using POS.DataLayer.Models;
using POS.Interface.DTO;
using POS.Interface.interfaces;
using System.Security.Claims;

namespace POS.system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _user;

        public UserController(IUserService user)
        {
            _user = user;
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            var res = await _user.test();

            if (res == null)
            {
                return BadRequest("error occured");
            }

            return Ok(res);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SignupDTO dto)
        {
            if (dto == null) return BadRequest("Invalid data");

            var res = await _user.SignUp(dto);

            if(!res)
            {
                return BadRequest("error occured");
            }
            return Ok(new { Message = "User created successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReqDTO dto)
        {
            if (dto == null) return Unauthorized("Invalid data");
            var res = await _user.Login(dto);
            if (res==null)
            {
                return BadRequest("error occured");
            }
            return Ok(res);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenRequestDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data");

            var res = await _user.Refresh(dto);

            if (res == null)
                return Unauthorized("Invalid refresh token");

            return Ok(res);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(RefreshTokenRequestDto dto)
        {

            if (dto == null)
                return BadRequest("Invalid data");

            var res = await _user.Logout(dto);

            if (!res)
                return Unauthorized("Invalid refresh token");

            return Ok(new { message = "Logout successful" });
        }
    }
}