using System;
using System.ComponentModel.DataAnnotations;

namespace POS.DataLayer.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        // Role Information
        [Required]
        [MaxLength(50)]
        public string RoleName { get; set; } = string.Empty;

        // Status
        public bool IsActive { get; set; } = true;


        public DateTime AddedOn { get; set; } = DateTime.UtcNow;

        // Soft Delete
        public int? DeletedBy { get; set; }

        public User? DeletedByUser { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}