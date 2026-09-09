using System;
using System.ComponentModel.DataAnnotations;

namespace POS.DataLayer.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [MaxLength(20)]
        public string PhoneNo { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        // Role
        public int RoleId { get; set; } = 3;

        public Role Role { get; set; } = null!;

        // Role updated by
        public DateTime? RoleUpdatedAt { get; set; }

        public int? RoleUpdatedBy { get; set; }

        public User? RoleUpdatedByUser { get; set; }

        // Soft Delete
        public int? DeletedBy { get; set; }

        public User? DeletedByUser { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}