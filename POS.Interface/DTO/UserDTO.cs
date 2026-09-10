using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;

namespace POS.Interface.DTO
{
    public class UserDTO
    {
            public int Id { get; set; }

            public string Name { get; set; } = string.Empty;

            public string Email { get; set; } = string.Empty;

            public string PasswordHash { get; set; } = string.Empty;

            public string PhoneNo { get; set; } = string.Empty;

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

            public bool IsActive { get; set; } = true;

            public int RoleId { get; set; } = 3;

        }
    
}
