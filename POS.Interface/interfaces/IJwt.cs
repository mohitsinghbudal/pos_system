using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Interface.interfaces
{
    public interface IJwt
    {
        string GenerateAccessToken(User user);

        string GenerateRefreshToken();

        DateTime GetAccessTokenExpiration();

        DateTime GetRefreshTokenExpiration();
    }
}
