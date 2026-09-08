using Microsoft.EntityFrameworkCore;
using POS.DataLayer.Data;
using POS.Interface.DTO;
using POS.Interface.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.DataLayer.DataAccess.RolesDAL
{
    public class RolesDll : IRolesDll
    {
        private readonly POSDbContext _context;
        public RolesDll(POSDbContext context)
        {

            _context = context;
        }
        public async Task<List<RolesDTO>> GetRolesAsync()
        {
            List<RolesDTO> roles = await _context.Roles
                .AsNoTracking()
                .Select(r=>new RolesDTO
                {
                    Id = r.Id,
                    Name = r.RoleName
                })
                .ToListAsync();

            return roles;
        }

        public async Task<bool> CreateRoleAsync(string role)
        {
            return true;
        }
    }
}
