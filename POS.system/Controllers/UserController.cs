using Microsoft.AspNetCore.Mvc;
using POS.DataLayer.Models;
using POS.Interface.interfaces;
using POS.Interface.DTO;

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
        public async Task<IActionResult> signUp(SignupDTO dto)
        {
            if (dto == null) return BadRequest("Invalid data");

            var res = await _user.signUp(dto);

            if(!res)
            {
                return BadRequest("error occured");
            }
            return Ok(res);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (dto == null) return BadRequest("Invalid data");
            var res = await _user.Login(dto);
            if (!res)
            {
                return BadRequest("error occured");
            }
            return Ok(res);
        }
    }
}