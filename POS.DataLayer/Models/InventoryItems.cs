using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.DataLayer.Models
{
    public class InventoryItem
    {
        [Key]
        public int Id { get; set; }


        // Product Information
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;


        // SubCategory
        [Required]
        public int SubCategoryId { get; set; }

        [ForeignKey(nameof(SubCategoryId))]
        public SubCategory SubCategory { get; set; } = null!;


        // Unit
        [Required]
        public int UnitId { get; set; }

        [ForeignKey(nameof(UnitId))]
        public Unit Unit { get; set; } = null!;


        // Inventory
        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }


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