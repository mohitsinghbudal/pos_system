
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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


        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        public Role Role { get; set; } = null!;

   
        public DateTime? RoleUpdatedAt { get; set; }

        public int? RoleUpdatedBy { get; set; }

        [ForeignKey(nameof(RoleUpdatedBy))]
        public User? RoleUpdatedByUser { get; set; }
    }
}
