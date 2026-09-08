using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using POS.Interface.interfaces;

namespace POS.system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _roles;
        public RolesController(IRolesService roles)
        {
            roles = _roles;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var res =  await _roles.GetRolesAsync();
            //if (!res.()) return BadRequest("error occured");
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Role name cannot be empty.");
            }
            var result = await _roles.CreateRoleAsync(roleName);
            if (!result)
            {
                return BadRequest("Failed to create role.");
            }
            return Ok($"Role '{roleName}' created successfully.");
        }

    }
}
