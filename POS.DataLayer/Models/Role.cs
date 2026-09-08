using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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


        // Audit - Created
        public int AddedBy { get; set; }

        [ForeignKey(nameof(AddedBy))]
        public User AddedByUser { get; set; } = null!;

        public DateTime AddedOn { get; set; } = DateTime.UtcNow;


        // Soft Delete
        public int? DeletedBy { get; set; }

        [ForeignKey(nameof(DeletedBy))]
        public User? DeletedByUser { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}