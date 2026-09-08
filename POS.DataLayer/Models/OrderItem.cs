using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.DataLayer.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }


        // Inventory Item / Product
        [Required]
        public int InventoryItemId { get; set; }

        [ForeignKey(nameof(InventoryItemId))]
        public InventoryItem InventoryItem { get; set; } = null!;


        // Quantity
        [Required]
        public decimal Quantity { get; set; }


        // Price at the time of ordering
        [Required]
        public decimal UnitPrice { get; set; }


        // Quantity × UnitPrice
        [Required]
        public decimal TotalPrice { get; set; }


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

        public DateTime? DeletedAt { get; set; }
    }
}