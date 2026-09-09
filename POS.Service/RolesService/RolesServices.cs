using POS.Interface.DTO;
using POS.Interface.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Service.rolesService
{
    public class RolesServcie : IRolesService
    {
        private readonly IRolesDll _rolesDll;
        public RolesServcie(IRolesDll rolesDll)
        {
            _rolesDll = rolesDll;
        }

        public async Task<List<RolesDTO>> GetRolesAsync()
        {
            return await _rolesDll.GetRolesAsync();
        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            return await _rolesDll.CreateRoleAsync(roleName);
        }

    }
}
