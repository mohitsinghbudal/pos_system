using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Interface.DTO
{
    public class RefreshTokenRequestDto
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
    public class RefreshTokenResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public DateTime AccessTokenExpiresAt { get; set; }

        public DateTime RefreshTokenExpiresAt { get; set; }
    }
}
