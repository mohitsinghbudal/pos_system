using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.DataLayer.Models
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }


        // Bill Information
        [Required]
        [MaxLength(50)]
        public string BillNumber { get; set; } = string.Empty;

        [Required]
        public DateTime BillDate { get; set; } = DateTime.UtcNow;


        // User
        public int? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }


        // Customer
        public int? CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public User? Customer { get; set; }


        // Inventory Items
        public ICollection<InventoryItem> InventoryItems { get; set; }
            = new List<InventoryItem>();


        // Amount
        [Required]
        public decimal SubTotal { get; set; }

        public decimal Discount { get; set; } = 0;

        public decimal Tax { get; set; } = 0;

        [Required]
        public decimal TotalAmount { get; set; }


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


        // Payments
        public ICollection<Payment> Payments { get; set; }
            = new List<Payment>();
    }
}