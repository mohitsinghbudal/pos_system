using Microsoft.EntityFrameworkCore;
using POS.DataLayer.Data;
using POS.DataLayer.Models;
using POS.Interface.DTO;
using POS.Interface.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.DataLayer.DataAccess.JwtDAL
{
    public class JwtDll : IJwtDll
    {
        private readonly POSDbContext _context;
        public JwtDll(POSDbContext context)
        {
            _context = context;

        }

        public async Task<RefreshTokenDto?> GetRefreshTokenAsync(string token)
        {
            var refreshToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.Token == token);

            if (refreshToken == null)
                return null;

            return new RefreshTokenDto
            {
                Token = refreshToken.Token,
                UserId = refreshToken.UserId,
                CreatedAt = refreshToken.CreatedAt,
                ExpiresAt = refreshToken.ExpiresAt,
                IsRevoked = refreshToken.IsRevoked,
                RevokedAt = refreshToken.RevokedAt,
                ReplacedByToken = refreshToken.ReplacedByToken
            };
        }

        public async Task<bool> AddRefreshTokenAsync(
            string refreshToken, int userId)
        {
            var newRefreshToken = new RefreshToken
            {
                Token = refreshToken,
                UserId = userId
            };

            await _context.RefreshTokens.AddAsync(newRefreshToken);
            return true;
        }

        public async Task<bool> UpdateRefreshToken(RefreshTokenDto dto)
        {
            var newitem = new RefreshToken
            {
                Id = dto.Id,
                Token = dto.Token,
                UserId = dto.UserId,
                CreatedAt = dto.CreatedAt,
                ExpiresAt = dto.ExpiresAt,
                IsRevoked = dto.IsRevoked,
                RevokedAt = dto.RevokedAt,
                ReplacedByToken = dto.ReplacedByToken
            };

            _context.RefreshTokens.Update(newitem);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
