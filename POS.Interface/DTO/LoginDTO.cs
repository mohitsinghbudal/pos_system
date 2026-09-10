using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Interface.DTO
{
    public class LoginReqDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    public class LoginResDTO
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
     
    }
}
