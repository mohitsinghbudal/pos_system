
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using POS.DataLayer.Models;

namespace POS.DataLayer.Models
{
    public class InventoryItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        public int SubCategoryId { get; set; }

        public SubCategory SubCategory { get; set; } = null!;

        
        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int CreatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public User CreatedByUser { get; set; } = null!;

        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(UpdatedBy))]
        public User? UpdatedByUser { get; set; }

        public bool IsActive { get; set; } = true;
    }
}

