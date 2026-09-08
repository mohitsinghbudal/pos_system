using POS.Interface.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Interface.interfaces
{
    public interface IRolesService
    {
        Task<List<RolesDTO>> GetRolesAsync();
        Task<bool> CreateRoleAsync(string roleName);
    }
    public interface IRolesDll
    {
        Task<List<RolesDTO>> GetRolesAsync();
        Task<bool> CreateRoleAsync(string roleName);
    }
}
