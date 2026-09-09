using Microsoft.EntityFrameworkCore;
using POS.DataLayer.Data;
using POS.DataLayer.Models;
using POS.Interface.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.DataLayer.DataAccess.JwtDAL
{
    internal class JwtDll : IJwtDll
    {
        private readonly POSDbContext _context;
        public JwtDll(POSDbContext context)
        {
            _context = context;

        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(
            string token)
        {
            return await _context.RefreshTokens
                .Include(rt => rt.User)
                .ThenInclude(u => u.Role)
                .FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task AddRefreshTokenAsync(
            RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
        }

        public Task UpdateRefreshTokenAsync(
            RefreshToken refreshToken)
        {
            _context.RefreshTokens.Update(refreshToken);

            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
