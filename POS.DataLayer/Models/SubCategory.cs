using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.DataLayer.Models
{
    public class SubCategory
    {
        [Key]
        public int Id { get; set; }


        // SubCategory Information
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;


        // Foreign Key → Category
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;


        // Audit - Created
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int CreatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public User CreatedByUser { get; set; } = null!;


        // Audit - Updated
        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(UpdatedBy))]
        public User? UpdatedByUser { get; set; }


        // Soft Delete
        public bool IsActive { get; set; } = true;

        public int? DeletedBy { get; set; }

        [ForeignKey(nameof(DeletedBy))]
        public User? DeletedByUser { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}