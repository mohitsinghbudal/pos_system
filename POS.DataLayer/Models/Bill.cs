
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using POS.DataLayer.Models;

namespace POS.DataLayer.Models
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string BillNumber { get; set; } = string.Empty;

        [Required]
        public DateTime BillDate { get; set; } = DateTime.UtcNow;

        // User whose bill this is
        public int? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        // Customer
        public int? CustomerId { get; set; }

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

        // Audit - who created the bill
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int CreatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public User CreatedByUser { get; set; } = null!;

        // Audit - who last updated the bill
        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(UpdatedBy))]
        public User? UpdatedByUser { get; set; }

        public bool IsActive { get; set; } = true;

        // Payments
        public ICollection<Payment> Payments { get; set; }
            = new List<Payment>();
    }
}
